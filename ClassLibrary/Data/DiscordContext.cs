using ClassLibrary.Models;
using ClassLibrary.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Chattr.Data
{
    public class ChattrContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendLog> FriendLogs { get; set; }

        public DbSet<Server> Servers { get; set; }
        public DbSet<MemberOfServer> Members { get; set; }
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }

        public ChattrContext(DbContextOptions<ChattrContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<Settings>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Friendship>()
                .HasKey(f => new { f.User1Id, f.User2Id});

            modelBuilder.Entity<Friendship>()
                .HasOne<User>(f => f.User1)
                .WithMany(u => u.FirstFriend)
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne<User>(f => f.User2)
                .WithMany(u => u.SecondFriend)
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FriendLog>()
                .HasOne<Friendship>(fl => fl.Friendship)
                .WithMany(f => f.Logs)
                .HasPrincipalKey(f => f.Id)
                .HasForeignKey(fl => fl.FriendshipId);


            modelBuilder.Entity<MemberOfServer>()
                .HasKey(ms => new { ms.UserId, ms.ServerId });

            modelBuilder.Entity<MemberOfServer>()
                .HasOne<User>(ms => ms.User)
                .WithMany(u => u.Servers)
                .HasForeignKey(ms => ms.UserId);

            modelBuilder.Entity<MemberOfServer>()
                .HasOne<Server>(ms => ms.Server)
                .WithMany(s => s.Users)
                .HasForeignKey(ms => ms.ServerId);


            modelBuilder.Entity<Server>()
                .HasMany<Chat>(s => s.Chats)
                .WithOne(c => c.Server)
                .HasForeignKey(c => c.ServerId);


            modelBuilder.Entity<Chat>()
                .HasMany<ChatLog>(c => c.Logs)
                .WithOne(cl => cl.Chat)
                .HasForeignKey(cl => cl.ChatId);

            modelBuilder.Entity<ChatLog>()
                .HasOne<User>(cl => cl.Sender)
                .WithMany(u => u.ThreadMessages)
                .HasForeignKey(cl => cl.SenderId);


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added || e.State == EntityState.Modified
                ));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).DateModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added || e.State == EntityState.Modified
                ));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).DateModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

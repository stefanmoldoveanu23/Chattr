using Discord_Copycat.Models;
using Microsoft.EntityFrameworkCore;

namespace Discord_Copycat.Data
{
    public class DiscordContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendLog> FriendLogs { get; set; }

        public DbSet<Server> Servers { get; set; }
        public DbSet<MemberOfServer> Members { get; set; }
        
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }

        public DiscordContext(DbContextOptions<DiscordContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<Settings>(s => s.UserId);

            modelBuilder.Entity<Friendship>()
                .HasKey(f => new { f.User1Id, f.User2Id});

            modelBuilder.Entity<Friendship>()
                .HasOne<User>(f => f.User1)
                .WithMany(u => u.FirstFriend)
                .HasForeignKey(f => f.User1Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Friendship>()
                .HasOne<User>(f => f.User2)
                .WithMany(u => u.SecondFriend)
                .HasForeignKey(f => f.User2Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

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
    }
}

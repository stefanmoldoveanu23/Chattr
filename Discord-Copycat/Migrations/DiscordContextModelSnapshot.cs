﻿// <auto-generated />
using System;
using Discord_Copycat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiscordCopycat.Migrations
{
    [DbContext(typeof(DiscordContext))]
    partial class DiscordContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Discord_Copycat.Models.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServerId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Discord_Copycat.Models.ChatLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.ToTable("ChatLogs");
                });

            modelBuilder.Entity("Discord_Copycat.Models.FriendLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FriendshipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FriendshipId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendLogs");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Friendship", b =>
                {
                    b.Property<Guid?>("User1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("User2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("User1Id", "User2Id");

                    b.HasIndex("User2Id");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Discord_Copycat.Models.MemberOfServer", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ServerId");

                    b.HasIndex("ServerId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Appearance")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Notifs")
                        .HasColumnType("bit");

                    b.Property<int>("Theme")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Discord_Copycat.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Chat", b =>
                {
                    b.HasOne("Discord_Copycat.Models.Server", "Server")
                        .WithMany("Chats")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");
                });

            modelBuilder.Entity("Discord_Copycat.Models.ChatLog", b =>
                {
                    b.HasOne("Discord_Copycat.Models.Chat", "Chat")
                        .WithMany("Logs")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Discord_Copycat.Models.User", "Sender")
                        .WithMany("ThreadMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Discord_Copycat.Models.FriendLog", b =>
                {
                    b.HasOne("Discord_Copycat.Models.Friendship", "Friendship")
                        .WithMany("Logs")
                        .HasForeignKey("FriendshipId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Discord_Copycat.Models.User", "Sender")
                        .WithMany("FriendMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Friendship");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Friendship", b =>
                {
                    b.HasOne("Discord_Copycat.Models.User", "User1")
                        .WithMany("FirstFriend")
                        .HasForeignKey("User1Id")
                        .IsRequired();

                    b.HasOne("Discord_Copycat.Models.User", "User2")
                        .WithMany("SecondFriend")
                        .HasForeignKey("User2Id")
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Discord_Copycat.Models.MemberOfServer", b =>
                {
                    b.HasOne("Discord_Copycat.Models.Server", "Server")
                        .WithMany("Users")
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Discord_Copycat.Models.User", "User")
                        .WithMany("Servers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Server");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Settings", b =>
                {
                    b.HasOne("Discord_Copycat.Models.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("Discord_Copycat.Models.Settings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Chat", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Friendship", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("Discord_Copycat.Models.Server", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Discord_Copycat.Models.User", b =>
                {
                    b.Navigation("FirstFriend");

                    b.Navigation("FriendMessages");

                    b.Navigation("SecondFriend");

                    b.Navigation("Servers");

                    b.Navigation("Settings")
                        .IsRequired();

                    b.Navigation("ThreadMessages");
                });
#pragma warning restore 612, 618
        }
    }
}

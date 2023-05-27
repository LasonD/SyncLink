﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SyncLink.Infrastructure.Data.Context;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    [DbContext(typeof(SyncLinkDbContext))]
    [Migration("20230527154037_AddPlotGame")]
    partial class AddPlotGame
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Associations.UserGroup", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCreator")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UsersToGroups");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Associations.UserRoom", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("UsersToRooms");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("TextPlotEntries");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GroupId");

                    b.ToTable("TextPlotGames");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntryId")
                        .HasColumnType("int");

                    b.Property<int?>("TextPlotEntryId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("TextPlotEntryId");

                    b.HasIndex("UserId");

                    b.ToTable("TextPlotVotes");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.Whiteboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Whiteboards");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EditedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("bit");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Associations.UserGroup", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SyncLink.Application.Domain.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Associations.UserRoom", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Room", "Room")
                        .WithMany("RoomMembers")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SyncLink.Application.Domain.User", "User")
                        .WithMany("UserRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotEntry", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotGame", "Game")
                        .WithMany("Entries")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SyncLink.Application.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotGame", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("SyncLink.Application.Domain.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotVote", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotEntry", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotEntry", null)
                        .WithMany("Votes")
                        .HasForeignKey("TextPlotEntryId");

                    b.HasOne("SyncLink.Application.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Entry");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.Whiteboard", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SyncLink.Application.Domain.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsMany("SyncLink.Application.Domain.Features.WhiteboardElement", "WhiteboardElements", b1 =>
                        {
                            b1.Property<int>("WhiteboardId")
                                .HasColumnType("int");

                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int?>("AuthorId")
                                .HasColumnType("int");

                            b1.Property<float>("Opacity")
                                .HasColumnType("real");

                            b1.Property<float>("Rotation")
                                .HasColumnType("real");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<float>("X")
                                .HasColumnType("real");

                            b1.Property<float>("Y")
                                .HasColumnType("real");

                            b1.HasKey("WhiteboardId", "Id");

                            b1.HasIndex("AuthorId");

                            b1.ToTable("WhiteboardElement");

                            b1.HasOne("SyncLink.Application.Domain.User", "Author")
                                .WithMany()
                                .HasForeignKey("AuthorId")
                                .OnDelete(DeleteBehavior.SetNull);

                            b1.WithOwner()
                                .HasForeignKey("WhiteboardId");

                            b1.OwnsOne("SyncLink.Application.Domain.Features.WhiteboardElementOptions", "Options", b2 =>
                                {
                                    b2.Property<int>("WhiteboardElementWhiteboardId")
                                        .HasColumnType("int");

                                    b2.Property<string>("WhiteboardElementId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<string>("Color")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<float?>("Cx")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Cy")
                                        .HasColumnType("real");

                                    b2.Property<string>("DashArray")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<float?>("DashOffset")
                                        .HasColumnType("real");

                                    b2.Property<string>("Fill")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("FontFamily")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<float?>("FontSize")
                                        .HasColumnType("real");

                                    b2.Property<string>("FontStyle")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("FontWeight")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<float?>("Height")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Left")
                                        .HasColumnType("real");

                                    b2.Property<int>("LineCap")
                                        .HasColumnType("int");

                                    b2.Property<int>("LineJoin")
                                        .HasColumnType("int");

                                    b2.Property<float?>("Rx")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Ry")
                                        .HasColumnType("real");

                                    b2.Property<string>("StrokeColor")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<float?>("StrokeWidth")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Top")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Width")
                                        .HasColumnType("real");

                                    b2.Property<float?>("X1")
                                        .HasColumnType("real");

                                    b2.Property<float?>("X2")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Y1")
                                        .HasColumnType("real");

                                    b2.Property<float?>("Y2")
                                        .HasColumnType("real");

                                    b2.HasKey("WhiteboardElementWhiteboardId", "WhiteboardElementId");

                                    b2.ToTable("WhiteboardElement");

                                    b2.WithOwner()
                                        .HasForeignKey("WhiteboardElementWhiteboardId", "WhiteboardElementId");
                                });

                            b1.Navigation("Author");

                            b1.Navigation("Options")
                                .IsRequired();
                        });

                    b.Navigation("Group");

                    b.Navigation("Owner");

                    b.Navigation("WhiteboardElements");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Message", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Room", "Room")
                        .WithMany("Messages")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SyncLink.Application.Domain.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Room", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.Group", "Group")
                        .WithMany("Rooms")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", b =>
                {
                    b.HasOne("SyncLink.Application.Domain.User", "ApplicationUser")
                        .WithOne()
                        .HasForeignKey("SyncLink.Infrastructure.Data.Models.Identity.SyncLinkIdentityUser", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotEntry", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Features.TextPlotGame.TextPlotGame", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Group", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.Room", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("RoomMembers");
                });

            modelBuilder.Entity("SyncLink.Application.Domain.User", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserGroups");

                    b.Navigation("UserRooms");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "ApplicationUsers",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetRoles",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Groups",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         IsPrivate = table.Column<bool>(type: "bit", nullable: false),
            //         CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Groups", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUsers",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         ApplicationUserId = table.Column<int>(type: "int", nullable: false),
            //         UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //         EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //         PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //         TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //         LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //         LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //         AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetUsers_ApplicationUsers_ApplicationUserId",
            //             column: x => x.ApplicationUserId,
            //             principalTable: "ApplicationUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetRoleClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Rooms",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         IsPrivate = table.Column<bool>(type: "bit", nullable: false),
            //         GroupId = table.Column<int>(type: "int", nullable: false),
            //         CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Rooms", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Rooms_Groups_GroupId",
            //             column: x => x.GroupId,
            //             principalTable: "Groups",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "UsersToGroups",
            //     columns: table => new
            //     {
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         GroupId = table.Column<int>(type: "int", nullable: false),
            //         IsCreator = table.Column<bool>(type: "bit", nullable: false),
            //         IsAdmin = table.Column<bool>(type: "bit", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_UsersToGroups", x => new { x.UserId, x.GroupId });
            //         table.ForeignKey(
            //             name: "FK_UsersToGroups_ApplicationUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "ApplicationUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_UsersToGroups_Groups_GroupId",
            //             column: x => x.GroupId,
            //             principalTable: "Groups",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserLogins",
            //     columns: table => new
            //     {
            //         LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserRoles",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserTokens",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //         Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Messages",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         EditedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         IsEdited = table.Column<bool>(type: "bit", nullable: false),
            //         Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         SenderId = table.Column<int>(type: "int", nullable: false),
            //         RoomId = table.Column<int>(type: "int", nullable: false),
            //         CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Messages", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Messages_ApplicationUsers_SenderId",
            //             column: x => x.SenderId,
            //             principalTable: "ApplicationUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_Messages_Rooms_RoomId",
            //             column: x => x.RoomId,
            //             principalTable: "Rooms",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "UsersToRooms",
            //     columns: table => new
            //     {
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         RoomId = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_UsersToRooms", x => new { x.UserId, x.RoomId });
            //         table.ForeignKey(
            //             name: "FK_UsersToRooms_ApplicationUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "ApplicationUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_UsersToRooms_Rooms_RoomId",
            //             column: x => x.RoomId,
            //             principalTable: "Rooms",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetRoleClaims_RoleId",
            //     table: "AspNetRoleClaims",
            //     column: "RoleId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "RoleNameIndex",
            //     table: "AspNetRoles",
            //     column: "NormalizedName",
            //     unique: true,
            //     filter: "[NormalizedName] IS NOT NULL");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserClaims_UserId",
            //     table: "AspNetUserClaims",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserLogins_UserId",
            //     table: "AspNetUserLogins",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserRoles_RoleId",
            //     table: "AspNetUserRoles",
            //     column: "RoleId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "EmailIndex",
            //     table: "AspNetUsers",
            //     column: "NormalizedEmail");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUsers_ApplicationUserId",
            //     table: "AspNetUsers",
            //     column: "ApplicationUserId",
            //     unique: true);
            //
            // migrationBuilder.CreateIndex(
            //     name: "UserNameIndex",
            //     table: "AspNetUsers",
            //     column: "NormalizedUserName",
            //     unique: true,
            //     filter: "[NormalizedUserName] IS NOT NULL");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Messages_RoomId",
            //     table: "Messages",
            //     column: "RoomId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Messages_SenderId",
            //     table: "Messages",
            //     column: "SenderId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Rooms_GroupId",
            //     table: "Rooms",
            //     column: "GroupId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_UsersToGroups_GroupId",
            //     table: "UsersToGroups",
            //     column: "GroupId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_UsersToRooms_RoomId",
            //     table: "UsersToRooms",
            //     column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropTable(
            //     name: "AspNetRoleClaims");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserClaims");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserLogins");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserRoles");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserTokens");
            //
            // migrationBuilder.DropTable(
            //     name: "Messages");
            //
            // migrationBuilder.DropTable(
            //     name: "UsersToGroups");
            //
            // migrationBuilder.DropTable(
            //     name: "UsersToRooms");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetRoles");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUsers");
            //
            // migrationBuilder.DropTable(
            //     name: "Rooms");
            //
            // migrationBuilder.DropTable(
            //     name: "ApplicationUsers");
            //
            // migrationBuilder.DropTable(
            //     name: "Groups");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discussions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordPhrase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionOrQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discussions_ApplicationUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discussions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votings_ApplicationUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Votings_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordsQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordsQuizzes_ApplicationUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WordsQuizzes_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VotingOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VotingId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotingOption_Votings_VotingId",
                        column: x => x.VotingId,
                        principalTable: "Votings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    VotingOptionId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_ApplicationUsers_VoterId",
                        column: x => x.VoterId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vote_VotingOption_VotingOptionId",
                        column: x => x.VotingOptionId,
                        principalTable: "VotingOption",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_AuthorId",
                table: "Discussions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_GroupId",
                table: "Discussions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoterId",
                table: "Vote",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VotingOptionId",
                table: "Vote",
                column: "VotingOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VotingOption_VotingId",
                table: "VotingOption",
                column: "VotingId");

            migrationBuilder.CreateIndex(
                name: "IX_Votings_AuthorId",
                table: "Votings",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Votings_GroupId",
                table: "Votings",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsQuizzes_AuthorId",
                table: "WordsQuizzes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsQuizzes_GroupId",
                table: "WordsQuizzes",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discussions");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "WordsQuizzes");

            migrationBuilder.DropTable(
                name: "VotingOption");

            migrationBuilder.DropTable(
                name: "Votings");
        }
    }
}

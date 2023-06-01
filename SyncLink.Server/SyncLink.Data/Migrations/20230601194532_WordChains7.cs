using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WordChains7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordsChainGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsChainGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordsChainGames_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersToWordsChains",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    IsCreator = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersToWordsChains", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UsersToWordsChains_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersToWordsChains_WordsChainGames_GameId",
                        column: x => x.GameId,
                        principalTable: "WordsChainGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordsChainEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsChainEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordsChainEntries_UsersToWordsChains_UserId_GameId",
                        columns: x => new { x.UserId, x.GameId },
                        principalTable: "UsersToWordsChains",
                        principalColumns: new[] { "UserId", "GameId" });
                    table.ForeignKey(
                        name: "FK_WordsChainEntries_WordsChainGames_GameId",
                        column: x => x.GameId,
                        principalTable: "WordsChainGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersToWordsChains_GameId",
                table: "UsersToWordsChains",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsChainEntries_GameId",
                table: "WordsChainEntries",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_WordsChainEntries_UserId_GameId",
                table: "WordsChainEntries",
                columns: new[] { "UserId", "GameId" });

            migrationBuilder.CreateIndex(
                name: "IX_WordsChainGames_GroupId",
                table: "WordsChainGames",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordsChainEntries");

            migrationBuilder.DropTable(
                name: "UsersToWordsChains");

            migrationBuilder.DropTable(
                name: "WordsChainGames");
        }
    }
}

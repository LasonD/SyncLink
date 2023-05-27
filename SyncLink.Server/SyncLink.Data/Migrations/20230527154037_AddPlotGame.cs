using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlotGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextPlotGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextPlotGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextPlotGames_ApplicationUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TextPlotGames_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextPlotEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextPlotEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextPlotEntries_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TextPlotEntries_TextPlotGames_GameId",
                        column: x => x.GameId,
                        principalTable: "TextPlotGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextPlotVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    EntryId = table.Column<int>(type: "int", nullable: false),
                    TextPlotEntryId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextPlotVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextPlotVotes_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TextPlotVotes_TextPlotEntries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "TextPlotEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextPlotVotes_TextPlotEntries_TextPlotEntryId",
                        column: x => x.TextPlotEntryId,
                        principalTable: "TextPlotEntries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotEntries_GameId",
                table: "TextPlotEntries",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotEntries_UserId",
                table: "TextPlotEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotGames_CreatorId",
                table: "TextPlotGames",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotGames_GroupId",
                table: "TextPlotGames",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotVotes_EntryId",
                table: "TextPlotVotes",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotVotes_TextPlotEntryId",
                table: "TextPlotVotes",
                column: "TextPlotEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_TextPlotVotes_UserId",
                table: "TextPlotVotes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextPlotVotes");

            migrationBuilder.DropTable(
                name: "TextPlotEntries");

            migrationBuilder.DropTable(
                name: "TextPlotGames");
        }
    }
}

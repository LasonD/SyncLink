using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "TextPlotVotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCommitted",
                table: "TextPlotEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscarded",
                table: "TextPlotEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "TextPlotVotes");

            migrationBuilder.DropColumn(
                name: "IsCommitted",
                table: "TextPlotEntries");

            migrationBuilder.DropColumn(
                name: "IsDiscarded",
                table: "TextPlotEntries");
        }
    }
}

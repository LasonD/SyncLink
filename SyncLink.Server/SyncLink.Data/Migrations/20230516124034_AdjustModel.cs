using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UsersToRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UsersToRooms");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");
        }
    }
}

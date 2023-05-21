using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWhiteboard2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_WhiteboardElement",
            //     table: "WhiteboardElement");
            //
            // migrationBuilder.DropColumn(
            //     name: "Id",
            //     table: "WhiteboardElement");
            //
            // migrationBuilder.AlterColumn<int>(
            //     name: "WhiteboardElementId",
            //     table: "WhiteboardElement",
            //     type: "int",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "int")
            //     .Annotation("SqlServer:Identity", "1, 1");
            //
            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_WhiteboardElement",
            //     table: "WhiteboardElement",
            //     columns: new[] { "WhiteboardId", "WhiteboardElementId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_WhiteboardElement",
            //     table: "WhiteboardElement");
            //
            // migrationBuilder.AlterColumn<int>(
            //     name: "WhiteboardElementId",
            //     table: "WhiteboardElement",
            //     type: "int",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "int")
            //     .OldAnnotation("SqlServer:Identity", "1, 1");
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Id",
            //     table: "WhiteboardElement",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     defaultValue: "");
            //
            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_WhiteboardElement",
            //     table: "WhiteboardElement",
            //     columns: new[] { "WhiteboardId", "Id" });
        }
    }
}

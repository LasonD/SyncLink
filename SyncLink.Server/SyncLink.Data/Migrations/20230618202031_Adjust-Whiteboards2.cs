using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustWhiteboards2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhiteboardElement_Whiteboards_WhiteboardId",
                table: "WhiteboardElement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhiteboardElement",
                table: "WhiteboardElement");

            migrationBuilder.AlterColumn<int>(
                name: "WhiteboardId",
                table: "WhiteboardElement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhiteboardElement",
                table: "WhiteboardElement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WhiteboardElement_WhiteboardId",
                table: "WhiteboardElement",
                column: "WhiteboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_WhiteboardElement_Whiteboards_WhiteboardId",
                table: "WhiteboardElement",
                column: "WhiteboardId",
                principalTable: "Whiteboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WhiteboardElement_Whiteboards_WhiteboardId",
                table: "WhiteboardElement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WhiteboardElement",
                table: "WhiteboardElement");

            migrationBuilder.DropIndex(
                name: "IX_WhiteboardElement_WhiteboardId",
                table: "WhiteboardElement");

            migrationBuilder.AlterColumn<int>(
                name: "WhiteboardId",
                table: "WhiteboardElement",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhiteboardElement",
                table: "WhiteboardElement",
                columns: new[] { "WhiteboardId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_WhiteboardElement_Whiteboards_WhiteboardId",
                table: "WhiteboardElement",
                column: "WhiteboardId",
                principalTable: "Whiteboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adjust123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Whiteboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whiteboards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhiteboardElement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WhiteboardId = table.Column<int>(type: "int", nullable: false),
                    WhiteboardElementId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Rotation = table.Column<int>(type: "int", nullable: false),
                    Opacity = table.Column<int>(type: "int", nullable: false),
                    Options_Width = table.Column<int>(type: "int", nullable: true),
                    Options_Height = table.Column<int>(type: "int", nullable: true),
                    Options_StrokeWidth = table.Column<int>(type: "int", nullable: true),
                    Options_StrokeColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_Fill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_LineJoin = table.Column<int>(type: "int", nullable: false),
                    Options_LineCap = table.Column<int>(type: "int", nullable: false),
                    Options_Left = table.Column<int>(type: "int", nullable: true),
                    Options_Top = table.Column<int>(type: "int", nullable: true),
                    Options_FontSize = table.Column<int>(type: "int", nullable: true),
                    Options_FontFamily = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_FontStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_FontWeight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_DashArray = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options_DashOffset = table.Column<int>(type: "int", nullable: true),
                    Options_X1 = table.Column<int>(type: "int", nullable: true),
                    Options_Y1 = table.Column<int>(type: "int", nullable: true),
                    Options_X2 = table.Column<int>(type: "int", nullable: true),
                    Options_Y2 = table.Column<int>(type: "int", nullable: true),
                    Options_Rx = table.Column<int>(type: "int", nullable: true),
                    Options_Ry = table.Column<int>(type: "int", nullable: true),
                    Options_Cx = table.Column<int>(type: "int", nullable: true),
                    Options_Cy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteboardElement", x => new { x.WhiteboardId, x.Id });
                    table.ForeignKey(
                        name: "FK_WhiteboardElement_Whiteboards_WhiteboardId",
                        column: x => x.WhiteboardId,
                        principalTable: "Whiteboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhiteboardElement");

            migrationBuilder.DropTable(
                name: "Whiteboards");
        }
    }
}

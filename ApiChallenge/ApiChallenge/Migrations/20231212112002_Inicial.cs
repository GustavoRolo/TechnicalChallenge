using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiChallenge.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recyclers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    run = table.Column<bool>(type: "INTEGER", nullable: false),
                    days = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recyclers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ip = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    port = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    path = table.Column<string>(type: "TEXT", nullable: false),
                    size = table.Column<long>(type: "INTEGER", nullable: false),
                    serverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    dateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Videos_Servers_serverId",
                        column: x => x.serverId,
                        principalTable: "Servers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recyclers",
                columns: new[] { "id", "days", "run" },
                values: new object[] { new Guid("bbc5a357-95c8-4712-ab96-5215c4a45f74"), 0, false });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_serverId",
                table: "Videos",
                column: "serverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recyclers");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Servers");
        }
    }
}

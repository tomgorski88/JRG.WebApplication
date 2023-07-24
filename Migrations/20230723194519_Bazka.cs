using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JRG.WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class Bazka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kursy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UkonczoneKursy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PracownikId = table.Column<int>(type: "INTEGER", nullable: false),
                    KursId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UkonczoneKursy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UkonczoneKursy_Kursy_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UkonczoneKursy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UkonczoneKursy_KursId",
                table: "UkonczoneKursy",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_UkonczoneKursy_PracownikId",
                table: "UkonczoneKursy",
                column: "PracownikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UkonczoneKursy");

            migrationBuilder.DropTable(
                name: "Kursy");
        }
    }
}

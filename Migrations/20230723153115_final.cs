using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JRG.WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prawko",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prawko", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stopnie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stopnie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zmiany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zmiany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Notatka = table.Column<string>(type: "TEXT", nullable: false),
                    DataUrodzenia = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DataZatrudnienia = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", nullable: false),
                    StopienId = table.Column<int>(type: "INTEGER", nullable: false),
                    ZmianaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Stopnie_StopienId",
                        column: x => x.StopienId,
                        principalTable: "Stopnie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Zmiany_ZmianaId",
                        column: x => x.ZmianaId,
                        principalTable: "Zmiany",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Uprawnienia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PracownikId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrawkoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uprawnienia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uprawnienia_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uprawnienia_Prawko_PrawkoId",
                        column: x => x.PrawkoId,
                        principalTable: "Prawko",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_StopienId",
                table: "Pracownicy",
                column: "StopienId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_ZmianaId",
                table: "Pracownicy",
                column: "ZmianaId");

            migrationBuilder.CreateIndex(
                name: "IX_Uprawnienia_PracownikId",
                table: "Uprawnienia",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Uprawnienia_PrawkoId",
                table: "Uprawnienia",
                column: "PrawkoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uprawnienia");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Prawko");

            migrationBuilder.DropTable(
                name: "Stopnie");

            migrationBuilder.DropTable(
                name: "Zmiany");
        }
    }
}

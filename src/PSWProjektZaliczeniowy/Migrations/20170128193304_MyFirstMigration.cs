using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PSWProjektZaliczeniowy.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    KategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.KategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    UzytkownikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adres = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Haslo = table.Column<string>(nullable: true),
                    Imie = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    Nrtel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.UzytkownikId);
                });

            migrationBuilder.CreateTable(
                name: "Podkategoria",
                columns: table => new
                {
                    PodkategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KategoriaId = table.Column<int>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podkategoria", x => x.PodkategoriaId);
                    table.ForeignKey(
                        name: "FK_Podkategoria_Kategoria_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategoria",
                        principalColumn: "KategoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Uzreceiver",
                columns: table => new
                {
                    UzreceiverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzreceiver", x => x.UzreceiverId);
                    table.ForeignKey(
                        name: "FK_Uzreceiver_Uzytkownik_UzreceiverId",
                        column: x => x.UzreceiverId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uzsender",
                columns: table => new
                {
                    UzsenderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzsender", x => x.UzsenderId);
                    table.ForeignKey(
                        name: "FK_Uzsender_Uzytkownik_UzsenderId",
                        column: x => x.UzsenderId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenie",
                columns: table => new
                {
                    OgloszenieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cena = table.Column<double>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    PodkategoriaId = table.Column<int>(nullable: true),
                    Stan = table.Column<string>(nullable: true),
                    Tytul = table.Column<string>(nullable: true),
                    UzytkownikId = table.Column<int>(nullable: true),
                    Zamiana = table.Column<bool>(nullable: false),
                    Zdjecia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogloszenie", x => x.OgloszenieId);
                    table.ForeignKey(
                        name: "FK_Ogloszenie_Podkategoria_PodkategoriaId",
                        column: x => x.PodkategoriaId,
                        principalTable: "Podkategoria",
                        principalColumn: "PodkategoriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ogloszenie_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wiadomosc",
                columns: table => new
                {
                    WiadomoscId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    Tekst = table.Column<string>(nullable: true),
                    UzreceiverId = table.Column<int>(nullable: true),
                    UzsenderId = table.Column<int>(nullable: true),
                    UzytkownikId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wiadomosc", x => x.WiadomoscId);
                    table.ForeignKey(
                        name: "FK_Wiadomosc_Uzreceiver_UzreceiverId",
                        column: x => x.UzreceiverId,
                        principalTable: "Uzreceiver",
                        principalColumn: "UzreceiverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wiadomosc_Uzsender_UzsenderId",
                        column: x => x.UzsenderId,
                        principalTable: "Uzsender",
                        principalColumn: "UzsenderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wiadomosc_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obserwowane",
                columns: table => new
                {
                    ObserwowaneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OgloszenieId = table.Column<int>(nullable: true),
                    UzytkownikId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obserwowane", x => x.ObserwowaneId);
                    table.ForeignKey(
                        name: "FK_Obserwowane_Ogloszenie_OgloszenieId",
                        column: x => x.OgloszenieId,
                        principalTable: "Ogloszenie",
                        principalColumn: "OgloszenieId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obserwowane_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "UzytkownikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obserwowane_OgloszenieId",
                table: "Obserwowane",
                column: "OgloszenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Obserwowane_UzytkownikId",
                table: "Obserwowane",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenie_PodkategoriaId",
                table: "Ogloszenie",
                column: "PodkategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenie_UzytkownikId",
                table: "Ogloszenie",
                column: "UzytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_Podkategoria_KategoriaId",
                table: "Podkategoria",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_UzreceiverId",
                table: "Wiadomosc",
                column: "UzreceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_UzsenderId",
                table: "Wiadomosc",
                column: "UzsenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_UzytkownikId",
                table: "Wiadomosc",
                column: "UzytkownikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obserwowane");

            migrationBuilder.DropTable(
                name: "Wiadomosc");

            migrationBuilder.DropTable(
                name: "Ogloszenie");

            migrationBuilder.DropTable(
                name: "Uzreceiver");

            migrationBuilder.DropTable(
                name: "Uzsender");

            migrationBuilder.DropTable(
                name: "Podkategoria");

            migrationBuilder.DropTable(
                name: "Uzytkownik");

            migrationBuilder.DropTable(
                name: "Kategoria");
        }
    }
}

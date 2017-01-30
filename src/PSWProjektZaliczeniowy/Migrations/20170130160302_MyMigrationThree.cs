using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSWProjektZaliczeniowy.Migrations
{
    public partial class MyMigrationThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogloszenie_Podkategoria_PodkategoriaId",
                table: "Ogloszenie");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogloszenie_Uzytkownik_UzytkownikId",
                table: "Ogloszenie");

            migrationBuilder.DropForeignKey(
                name: "FK_Podkategoria_Kategoria_KategoriaId",
                table: "Podkategoria");

            migrationBuilder.AddColumn<bool>(
                name: "Odczytana",
                table: "Wiadomosc",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UzytkownikId",
                table: "Uzsender",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UzytkownikId",
                table: "Uzreceiver",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "KategoriaId",
                table: "Podkategoria",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UzytkownikId",
                table: "Ogloszenie",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PodkategoriaId",
                table: "Ogloszenie",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogloszenie_Podkategoria_PodkategoriaId",
                table: "Ogloszenie",
                column: "PodkategoriaId",
                principalTable: "Podkategoria",
                principalColumn: "PodkategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogloszenie_Uzytkownik_UzytkownikId",
                table: "Ogloszenie",
                column: "UzytkownikId",
                principalTable: "Uzytkownik",
                principalColumn: "UzytkownikId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Podkategoria_Kategoria_KategoriaId",
                table: "Podkategoria",
                column: "KategoriaId",
                principalTable: "Kategoria",
                principalColumn: "KategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ogloszenie_Podkategoria_PodkategoriaId",
                table: "Ogloszenie");

            migrationBuilder.DropForeignKey(
                name: "FK_Ogloszenie_Uzytkownik_UzytkownikId",
                table: "Ogloszenie");

            migrationBuilder.DropForeignKey(
                name: "FK_Podkategoria_Kategoria_KategoriaId",
                table: "Podkategoria");

            migrationBuilder.DropColumn(
                name: "Odczytana",
                table: "Wiadomosc");

            migrationBuilder.DropColumn(
                name: "UzytkownikId",
                table: "Uzsender");

            migrationBuilder.DropColumn(
                name: "UzytkownikId",
                table: "Uzreceiver");

            migrationBuilder.AlterColumn<int>(
                name: "KategoriaId",
                table: "Podkategoria",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UzytkownikId",
                table: "Ogloszenie",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PodkategoriaId",
                table: "Ogloszenie",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ogloszenie_Podkategoria_PodkategoriaId",
                table: "Ogloszenie",
                column: "PodkategoriaId",
                principalTable: "Podkategoria",
                principalColumn: "PodkategoriaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ogloszenie_Uzytkownik_UzytkownikId",
                table: "Ogloszenie",
                column: "UzytkownikId",
                principalTable: "Uzytkownik",
                principalColumn: "UzytkownikId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Podkategoria_Kategoria_KategoriaId",
                table: "Podkategoria",
                column: "KategoriaId",
                principalTable: "Kategoria",
                principalColumn: "KategoriaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSWProjektZaliczeniowy.Migrations
{
    public partial class Migracja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wiadomosc_Uzytkownik_UzytkownikId",
                table: "Wiadomosc");

            migrationBuilder.DropIndex(
                name: "IX_Wiadomosc_UzytkownikId",
                table: "Wiadomosc");

            migrationBuilder.DropColumn(
                name: "UzytkownikId",
                table: "Wiadomosc");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Uzytkownik",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Haslo",
                table: "Uzytkownik",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UzytkownikId",
                table: "Wiadomosc",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Uzytkownik",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Haslo",
                table: "Uzytkownik",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_UzytkownikId",
                table: "Wiadomosc",
                column: "UzytkownikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wiadomosc_Uzytkownik_UzytkownikId",
                table: "Wiadomosc",
                column: "UzytkownikId",
                principalTable: "Uzytkownik",
                principalColumn: "UzytkownikId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

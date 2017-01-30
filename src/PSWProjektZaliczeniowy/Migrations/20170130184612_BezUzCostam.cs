using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSWProjektZaliczeniowy.Migrations
{
    public partial class BezUzCostam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wiadomosc_Uzreceiver_UzreceiverId",
                table: "Wiadomosc");

            migrationBuilder.DropForeignKey(
                name: "FK_Wiadomosc_Uzsender_UzsenderId",
                table: "Wiadomosc");

            migrationBuilder.DropTable(
                name: "Uzreceiver");

            migrationBuilder.DropTable(
                name: "Uzsender");

            migrationBuilder.DropIndex(
                name: "IX_Wiadomosc_UzreceiverId",
                table: "Wiadomosc");

            migrationBuilder.DropIndex(
                name: "IX_Wiadomosc_UzsenderId",
                table: "Wiadomosc");

            migrationBuilder.DropColumn(
                name: "UzreceiverId",
                table: "Wiadomosc");

            migrationBuilder.DropColumn(
                name: "UzsenderId",
                table: "Wiadomosc");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Wiadomosc",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Wiadomosc",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Wiadomosc");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Wiadomosc");

            migrationBuilder.AddColumn<int>(
                name: "UzreceiverId",
                table: "Wiadomosc",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UzsenderId",
                table: "Wiadomosc",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Uzreceiver",
                columns: table => new
                {
                    UzreceiverId = table.Column<int>(nullable: false),
                    UzytkownikId = table.Column<int>(nullable: false)
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
                    UzsenderId = table.Column<int>(nullable: false),
                    UzytkownikId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_UzreceiverId",
                table: "Wiadomosc",
                column: "UzreceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_UzsenderId",
                table: "Wiadomosc",
                column: "UzsenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wiadomosc_Uzreceiver_UzreceiverId",
                table: "Wiadomosc",
                column: "UzreceiverId",
                principalTable: "Uzreceiver",
                principalColumn: "UzreceiverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wiadomosc_Uzsender_UzsenderId",
                table: "Wiadomosc",
                column: "UzsenderId",
                principalTable: "Uzsender",
                principalColumn: "UzsenderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

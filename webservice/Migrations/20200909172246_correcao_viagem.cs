using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalizaVan.Migrations
{
    public partial class correcao_viagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Viagem");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Viagem");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fim",
                table: "Viagem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Finalizada",
                table: "Viagem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inicio",
                table: "Viagem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Viagem");

            migrationBuilder.DropColumn(
                name: "Finalizada",
                table: "Viagem");

            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Viagem");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Viagem",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Viagem",
                type: "text",
                nullable: true);
        }
    }
}

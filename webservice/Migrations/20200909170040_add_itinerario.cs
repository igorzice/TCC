using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LocalizaVan.Migrations
{
    public partial class add_itinerario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizacoes_Viagem_ViagemId",
                table: "Localizacoes");

            migrationBuilder.DropIndex(
                name: "IX_Localizacoes_ViagemId",
                table: "Localizacoes");

            migrationBuilder.DropColumn(
                name: "ViagemId",
                table: "Localizacoes");

            migrationBuilder.AddColumn<int>(
                name: "ItinerarioId",
                table: "Viagem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "Localizacoes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Itinerario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinerario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Viagem_ItinerarioId",
                table: "Viagem",
                column: "ItinerarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_VeiculoId",
                table: "Localizacoes",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacoes_Veiculos_VeiculoId",
                table: "Localizacoes",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Viagem_Itinerario_ItinerarioId",
                table: "Viagem",
                column: "ItinerarioId",
                principalTable: "Itinerario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizacoes_Veiculos_VeiculoId",
                table: "Localizacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Viagem_Itinerario_ItinerarioId",
                table: "Viagem");

            migrationBuilder.DropTable(
                name: "Itinerario");

            migrationBuilder.DropIndex(
                name: "IX_Viagem_ItinerarioId",
                table: "Viagem");

            migrationBuilder.DropIndex(
                name: "IX_Localizacoes_VeiculoId",
                table: "Localizacoes");

            migrationBuilder.DropColumn(
                name: "ItinerarioId",
                table: "Viagem");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "Localizacoes");

            migrationBuilder.AddColumn<int>(
                name: "ViagemId",
                table: "Localizacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_ViagemId",
                table: "Localizacoes",
                column: "ViagemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacoes_Viagem_ViagemId",
                table: "Localizacoes",
                column: "ViagemId",
                principalTable: "Viagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

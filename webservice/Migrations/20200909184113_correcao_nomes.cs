using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalizaVan.Migrations
{
    public partial class correcao_nomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viagem_Itinerario_ItinerarioId",
                table: "Viagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Viagem_Veiculos_VeiculoId",
                table: "Viagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viagem",
                table: "Viagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerario",
                table: "Itinerario");

            migrationBuilder.RenameTable(
                name: "Viagem",
                newName: "Viagens");

            migrationBuilder.RenameTable(
                name: "Itinerario",
                newName: "Itinerarios");

            migrationBuilder.RenameIndex(
                name: "IX_Viagem_VeiculoId",
                table: "Viagens",
                newName: "IX_Viagens_VeiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Viagem_ItinerarioId",
                table: "Viagens",
                newName: "IX_Viagens_ItinerarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viagens",
                table: "Viagens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerarios",
                table: "Itinerarios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Viagens_Itinerarios_ItinerarioId",
                table: "Viagens",
                column: "ItinerarioId",
                principalTable: "Itinerarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Viagens_Veiculos_VeiculoId",
                table: "Viagens",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viagens_Itinerarios_ItinerarioId",
                table: "Viagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Viagens_Veiculos_VeiculoId",
                table: "Viagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viagens",
                table: "Viagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Itinerarios",
                table: "Itinerarios");

            migrationBuilder.RenameTable(
                name: "Viagens",
                newName: "Viagem");

            migrationBuilder.RenameTable(
                name: "Itinerarios",
                newName: "Itinerario");

            migrationBuilder.RenameIndex(
                name: "IX_Viagens_VeiculoId",
                table: "Viagem",
                newName: "IX_Viagem_VeiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Viagens_ItinerarioId",
                table: "Viagem",
                newName: "IX_Viagem_ItinerarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viagem",
                table: "Viagem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Itinerario",
                table: "Itinerario",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Viagem_Itinerario_ItinerarioId",
                table: "Viagem",
                column: "ItinerarioId",
                principalTable: "Itinerario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Viagem_Veiculos_VeiculoId",
                table: "Viagem",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

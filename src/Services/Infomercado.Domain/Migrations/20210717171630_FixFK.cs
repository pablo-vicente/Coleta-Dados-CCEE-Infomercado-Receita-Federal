using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class FixFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_RepasseRiscoHidrologicos_Mes_IdPerfilAgente_IdParcelaUsina_Patamar_Semana",
                table: "RepasseRiscoHidrologicos");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RepasseRiscoHidrologicos_Mes_IdPerfilAgente_IdParcelaUsina_Patamar_Semana_TipoRepasseRiscoHidrologico",
                table: "RepasseRiscoHidrologicos",
                columns: new[] { "Mes", "IdPerfilAgente", "IdParcelaUsina", "Patamar", "Semana", "TipoRepasseRiscoHidrologico" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_RepasseRiscoHidrologicos_Mes_IdPerfilAgente_IdParcelaUsina_Patamar_Semana_TipoRepasseRiscoHidrologico",
                table: "RepasseRiscoHidrologicos");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RepasseRiscoHidrologicos_Mes_IdPerfilAgente_IdParcelaUsina_Patamar_Semana",
                table: "RepasseRiscoHidrologicos",
                columns: new[] { "Mes", "IdPerfilAgente", "IdParcelaUsina", "Patamar", "Semana" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class AddKeyDisponiblidadeLeilao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno_Produto",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno_Leilao_Produto",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades",
                columns: new[] { "IdParcelaUsina", "MesAno", "Leilao", "Produto" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno_Leilao_Produto",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno_Produto",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades",
                columns: new[] { "IdParcelaUsina", "MesAno", "Produto" });
        }
    }
}

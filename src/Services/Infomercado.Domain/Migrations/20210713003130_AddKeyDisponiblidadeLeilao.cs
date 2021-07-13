using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class AddKeyDisponiblidadeLeilao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno_Leilao",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades",
                columns: new[] { "IdParcelaUsina", "MesAno", "Leilao" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno_Leilao",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno",
                table: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades",
                columns: new[] { "IdParcelaUsina", "MesAno" });
        }
    }
}

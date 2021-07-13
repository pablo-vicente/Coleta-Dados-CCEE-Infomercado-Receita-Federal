using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class DisponiblidadeLeilao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_CotistasLiquidacaoDistribuidoraGarantiaFisicas_MesAno_IdParcelaUsina",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas");

            migrationBuilder.DropIndex(
                name: "IX_CotistasLiquidacaoDistribuidoraGarantiaFisicas_IdParcelaUsina",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas");

            migrationBuilder.AlterColumn<string>(
                name: "Ccve",
                table: "ProinfaInformacoesConformeResolucao1833Usinas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CotistasLiquidacaoDistribuidoraGarantiaFisicas_IdParcelaUsina_MesAno",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas",
                columns: new[] { "IdParcelaUsina", "MesAno" });

            migrationBuilder.CreateTable(
                name: "GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CegeEmpreendimento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GarantiaFisicaMWm = table.Column<double>(type: "float", nullable: false),
                    GarantiaFisicaComprometidaMWm = table.Column<double>(type: "float", nullable: false),
                    GeracaoMWm = table.Column<double>(type: "float", nullable: false),
                    GeracaoDestinadaLeilaoMWm = table.Column<double>(type: "float", nullable: false),
                    GeracaoLivreMWm = table.Column<double>(type: "float", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades", x => x.Id);
                    table.UniqueConstraint("AK_GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades_IdParcelaUsina_MesAno", x => new { x.IdParcelaUsina, x.MesAno });
                    table.ForeignKey(
                        name: "FK_GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CegeEmpreendimento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Leilao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Produto = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    SiglaLeilao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FonteEnergiaPrimaria = table.Column<int>(type: "int", nullable: false),
                    DataLeilao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GarantiaFisicaComprometidaMWm = table.Column<double>(type: "float", nullable: false),
                    ContratosMWh = table.Column<double>(type: "float", nullable: false),
                    GeracaoDestinadaLeilaoMWm = table.Column<double>(type: "float", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades", x => x.Id);
                    table.UniqueConstraint("AK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_IdParcelaUsina_MesAno", x => new { x.IdParcelaUsina, x.MesAno });
                    table.ForeignKey(
                        name: "FK_MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades");

            migrationBuilder.DropTable(
                name: "MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CotistasLiquidacaoDistribuidoraGarantiaFisicas_IdParcelaUsina_MesAno",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas");

            migrationBuilder.AlterColumn<string>(
                name: "Ccve",
                table: "ProinfaInformacoesConformeResolucao1833Usinas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CotistasLiquidacaoDistribuidoraGarantiaFisicas_MesAno_IdParcelaUsina",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas",
                columns: new[] { "MesAno", "IdParcelaUsina" });

            migrationBuilder.CreateIndex(
                name: "IX_CotistasLiquidacaoDistribuidoraGarantiaFisicas_IdParcelaUsina",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas",
                column: "IdParcelaUsina");
        }
    }
}

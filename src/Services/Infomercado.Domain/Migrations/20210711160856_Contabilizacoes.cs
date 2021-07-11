using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class Contabilizacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contabilizacaes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultadoMercadoCurtoPrazo = table.Column<double>(type: "float", nullable: false),
                    CompensacaoMre = table.Column<double>(type: "float", nullable: false),
                    EncargosConsolidados = table.Column<double>(type: "float", nullable: false),
                    AjusteExposicaoFinanceira = table.Column<double>(type: "float", nullable: false),
                    AjusteAlivioRetroativo = table.Column<double>(type: "float", nullable: false),
                    EfeitoContracaoPorDisponibilidade = table.Column<double>(type: "float", nullable: false),
                    EfeitoContratacaoCotaGarantiaFisica = table.Column<double>(type: "float", nullable: false),
                    EfeitoContratacaoComercializacaoEnergiaNuclear = table.Column<double>(type: "float", nullable: false),
                    AjusteRecontabilizacoes = table.Column<double>(type: "float", nullable: false),
                    AjusteMcsd = table.Column<double>(type: "float", nullable: false),
                    ExcedenteFinanceiroEnergiaReserva = table.Column<double>(type: "float", nullable: false),
                    EfeitoCcearUsinasAptas = table.Column<double>(type: "float", nullable: false),
                    EfeitoContratacaoItaipu = table.Column<double>(type: "float", nullable: false),
                    EfeitoRepasseRiscoHidrologico = table.Column<double>(type: "float", nullable: false),
                    EfeitoCustosDeslocamentoPldeCmo = table.Column<double>(type: "float", nullable: false),
                    ResultadoFinal = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contabilizacaes", x => x.Id);
                    table.UniqueConstraint("AK_Contabilizacaes_IdPerfilAgente_MesAno", x => new { x.IdPerfilAgente, x.MesAno });
                    table.ForeignKey(
                        name: "FK_Contabilizacaes_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contabilizacaes");
        }
    }
}

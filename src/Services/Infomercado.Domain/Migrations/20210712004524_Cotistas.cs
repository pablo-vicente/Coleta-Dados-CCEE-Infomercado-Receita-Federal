using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class Cotistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contabilizacaes_IdPerfilAgente_MesAno",
                table: "Contabilizacaes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contabilizacaes_MesAno_IdPerfilAgente",
                table: "Contabilizacaes",
                columns: new[] { "MesAno", "IdPerfilAgente" });

            migrationBuilder.CreateTable(
                name: "CotistasLiquidacaoDistribuidoraGarantiaFisicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FatorAdequacao = table.Column<double>(type: "float", nullable: false),
                    ReceitaFixaPreliminarRs = table.Column<double>(type: "float", nullable: false),
                    ReceitaFixaAjustadaRs = table.Column<double>(type: "float", nullable: false),
                    ReceitaFixaTotalRs = table.Column<double>(type: "float", nullable: false),
                    CustosAdministrativosRs = table.Column<double>(type: "float", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotistasLiquidacaoDistribuidoraGarantiaFisicas", x => x.Id);
                    table.UniqueConstraint("AK_CotistasLiquidacaoDistribuidoraGarantiaFisicas_MesAno_IdParcelaUsina", x => new { x.MesAno, x.IdParcelaUsina });
                    table.ForeignKey(
                        name: "FK_CotistasLiquidacaoDistribuidoraGarantiaFisicas_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaVendaComercializacaoEnergiaNucleares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FatorAdequacao = table.Column<double>(type: "float", nullable: false),
                    ReceitaFixaPreliminarRs = table.Column<double>(type: "float", nullable: false),
                    ReceitaFixaAjustadaRs = table.Column<double>(type: "float", nullable: false),
                    ReceitaVendaTotalRs = table.Column<double>(type: "float", nullable: false),
                    CustoAdministrativosRs = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaVendaComercializacaoEnergiaNucleares", x => x.Id);
                    table.UniqueConstraint("AK_ReceitaVendaComercializacaoEnergiaNucleares_MesAno_IdPerfilAgente", x => new { x.MesAno, x.IdPerfilAgente });
                    table.ForeignKey(
                        name: "FK_ReceitaVendaComercializacaoEnergiaNucleares_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaVendaDistribuidoraCotistaEnergiaNucleares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ajustes = table.Column<double>(type: "float", nullable: false),
                    FatorRateiroCotas = table.Column<double>(type: "float", nullable: false),
                    ReceitaVenda = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaVendaDistribuidoraCotistaEnergiaNucleares", x => x.Id);
                    table.UniqueConstraint("AK_ReceitaVendaDistribuidoraCotistaEnergiaNucleares_MesAno_IdPerfilAgente", x => new { x.MesAno, x.IdPerfilAgente });
                    table.ForeignKey(
                        name: "FK_ReceitaVendaDistribuidoraCotistaEnergiaNucleares_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaVendaDistribuidoraCotistaGarantiaFisicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesAno = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ajustes = table.Column<double>(type: "float", nullable: false),
                    FatorRateioCotas = table.Column<double>(type: "float", nullable: false),
                    ReceitaVenda = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaVendaDistribuidoraCotistaGarantiaFisicas", x => x.Id);
                    table.UniqueConstraint("AK_ReceitaVendaDistribuidoraCotistaGarantiaFisicas_MesAno_IdPerfilAgente_IdParcelaUsina", x => new { x.MesAno, x.IdPerfilAgente, x.IdParcelaUsina });
                    table.ForeignKey(
                        name: "FK_ReceitaVendaDistribuidoraCotistaGarantiaFisicas_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceitaVendaDistribuidoraCotistaGarantiaFisicas_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contabilizacaes_IdPerfilAgente",
                table: "Contabilizacaes",
                column: "IdPerfilAgente");

            migrationBuilder.CreateIndex(
                name: "IX_CotistasLiquidacaoDistribuidoraGarantiaFisicas_IdParcelaUsina",
                table: "CotistasLiquidacaoDistribuidoraGarantiaFisicas",
                column: "IdParcelaUsina");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaVendaComercializacaoEnergiaNucleares_IdPerfilAgente",
                table: "ReceitaVendaComercializacaoEnergiaNucleares",
                column: "IdPerfilAgente");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaVendaDistribuidoraCotistaEnergiaNucleares_IdPerfilAgente",
                table: "ReceitaVendaDistribuidoraCotistaEnergiaNucleares",
                column: "IdPerfilAgente");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaVendaDistribuidoraCotistaGarantiaFisicas_IdParcelaUsina",
                table: "ReceitaVendaDistribuidoraCotistaGarantiaFisicas",
                column: "IdParcelaUsina");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaVendaDistribuidoraCotistaGarantiaFisicas_IdPerfilAgente",
                table: "ReceitaVendaDistribuidoraCotistaGarantiaFisicas",
                column: "IdPerfilAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotistasLiquidacaoDistribuidoraGarantiaFisicas");

            migrationBuilder.DropTable(
                name: "ReceitaVendaComercializacaoEnergiaNucleares");

            migrationBuilder.DropTable(
                name: "ReceitaVendaDistribuidoraCotistaEnergiaNucleares");

            migrationBuilder.DropTable(
                name: "ReceitaVendaDistribuidoraCotistaGarantiaFisicas");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contabilizacaes_MesAno_IdPerfilAgente",
                table: "Contabilizacaes");

            migrationBuilder.DropIndex(
                name: "IX_Contabilizacaes_IdPerfilAgente",
                table: "Contabilizacaes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contabilizacaes_IdPerfilAgente_MesAno",
                table: "Contabilizacaes",
                columns: new[] { "IdPerfilAgente", "MesAno" });
        }
    }
}

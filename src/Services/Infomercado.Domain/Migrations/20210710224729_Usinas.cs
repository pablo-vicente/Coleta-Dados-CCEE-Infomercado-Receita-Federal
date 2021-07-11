using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class Usinas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoAtivo = table.Column<int>(type: "int", nullable: false),
                    SiglaAtivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Submercado = table.Column<int>(type: "int", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParcelaUsinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaracteristicaParcelaUsina = table.Column<int>(type: "int", nullable: false),
                    DataInícioOperacaoComercialCcee = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelaUsinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelaUsinas_Usinas_IdUsina",
                        column: x => x.IdUsina,
                        principalTable: "Usinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosGeracaoUsinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cegempreendimento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TipoDespacho = table.Column<int>(type: "int", nullable: false),
                    ParticipanteRateioPerdas = table.Column<bool>(type: "bit", nullable: false),
                    FonteEnergiaPrimaria = table.Column<int>(type: "int", nullable: false),
                    Combustivel = table.Column<int>(type: "int", nullable: true),
                    ParticipanteMre = table.Column<bool>(type: "bit", nullable: false),
                    ParticipanteRegimeCotas = table.Column<bool>(type: "bit", nullable: false),
                    TaxaDescontoUsina = table.Column<double>(type: "float", nullable: false),
                    CapacidadeUsinaMW = table.Column<double>(type: "float", nullable: false),
                    GarantiaFisicaMWm = table.Column<double>(type: "float", nullable: false),
                    FatorOperacaoComercial = table.Column<double>(type: "float", nullable: false),
                    FatorPerdasInternas = table.Column<double>(type: "float", nullable: false),
                    Mes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeracaoCentroGravidadeMWm = table.Column<double>(type: "float", nullable: false),
                    GeracaoTesteCentroGravidadeMWm = table.Column<double>(type: "float", nullable: false),
                    GarantiaFisicaCentroGravidadeMWm = table.Column<double>(type: "float", nullable: false),
                    GeracaoSegurancaEnergeticaMWm = table.Column<double>(type: "float", nullable: false),
                    GeracaoRestricaoOperacaoConstrainedOnMwm = table.Column<double>(type: "float", nullable: false),
                    GeracaoManutencaoReserveOperativaMWm = table.Column<double>(type: "float", nullable: false),
                    idParcelaUsina = table.Column<int>(type: "int", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosGeracaoUsinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadosGeracaoUsinas_ParcelaUsinas_idParcelaUsina",
                        column: x => x.idParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DadosGeracaoUsinas_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DadosGeracaoUsinas_idParcelaUsina",
                table: "DadosGeracaoUsinas",
                column: "idParcelaUsina");

            migrationBuilder.CreateIndex(
                name: "IX_DadosGeracaoUsinas_IdPerfilAgente",
                table: "DadosGeracaoUsinas",
                column: "IdPerfilAgente");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelaUsinas_IdUsina",
                table: "ParcelaUsinas",
                column: "IdUsina");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosGeracaoUsinas");

            migrationBuilder.DropTable(
                name: "ParcelaUsinas");

            migrationBuilder.DropTable(
                name: "Usinas");
        }
    }
}

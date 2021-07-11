using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class AddEncargos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RestricaoConstrainedOff = table.Column<double>(type: "float", nullable: false),
                    RestricaoConstrainedOn = table.Column<double>(type: "float", nullable: false),
                    CompensacaoSincrona = table.Column<double>(type: "float", nullable: false),
                    OutrosServicosAncilares = table.Column<double>(type: "float", nullable: false),
                    DespachoSegurancaEnergetica = table.Column<double>(type: "float", nullable: false),
                    DeslocamentoUsinaHidreletrica = table.Column<double>(type: "float", nullable: false),
                    RessarcimentoDeslocamento = table.Column<double>(type: "float", nullable: false),
                    RessarcimentoDespachoReservaPotenciaOperativa = table.Column<double>(type: "float", nullable: false),
                    EncargoImportacaoEnergia = table.Column<double>(type: "float", nullable: true),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encargos", x => x.Id);
                    table.UniqueConstraint("AK_Encargos_IdParcelaUsina_IdPerfilAgente_Mes", x => new { x.IdParcelaUsina, x.IdPerfilAgente, x.Mes });
                    table.ForeignKey(
                        name: "FK_Encargos_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encargos_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encargos_IdPerfilAgente",
                table: "Encargos",
                column: "IdPerfilAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Encargos");
        }
    }
}

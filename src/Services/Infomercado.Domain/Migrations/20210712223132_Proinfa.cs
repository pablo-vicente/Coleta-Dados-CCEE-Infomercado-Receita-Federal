using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class Proinfa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProinfaInformacoesConformeResolucao1833Usinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ccve = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FonteEnergiaPrimaria = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeracaoCentroGravidadeMWm = table.Column<double>(type: "float", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProinfaInformacoesConformeResolucao1833Usinas", x => x.Id);
                    table.UniqueConstraint("AK_ProinfaInformacoesConformeResolucao1833Usinas_IdParcelaUsina_Data", x => new { x.IdParcelaUsina, x.Data });
                    table.ForeignKey(
                        name: "FK_ProinfaInformacoesConformeResolucao1833Usinas_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipanteMreEUsina = table.Column<bool>(type: "bit", nullable: false),
                    FonteEnergiaPrimaria = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FluxoEnergiaMreMWm = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres", x => x.Id);
                    table.UniqueConstraint("AK_ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres_IdParcelaUsina_IdPerfilAgente_Data", x => new { x.IdParcelaUsina, x.IdPerfilAgente, x.Data });
                    table.ForeignKey(
                        name: "FK_ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres_IdPerfilAgente",
                table: "ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres",
                column: "IdPerfilAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProinfaInformacoesConformeResolucao1833Usinas");

            migrationBuilder.DropTable(
                name: "ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class RRH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepasseRiscoHidrologicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoRepasseRiscoHidrologico = table.Column<int>(type: "int", nullable: false),
                    IdParcelaUsina = table.Column<int>(type: "int", nullable: true),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false),
                    Semana = table.Column<int>(type: "int", nullable: true),
                    Patamar = table.Column<int>(type: "int", nullable: true),
                    Mes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RiscoHidrologico = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepasseRiscoHidrologicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepasseRiscoHidrologicos_ParcelaUsinas_IdParcelaUsina",
                        column: x => x.IdParcelaUsina,
                        principalTable: "ParcelaUsinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepasseRiscoHidrologicos_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepasseRiscoHidrologicos_IdParcelaUsina",
                table: "RepasseRiscoHidrologicos",
                column: "IdParcelaUsina");

            migrationBuilder.CreateIndex(
                name: "IX_RepasseRiscoHidrologicos_IdPerfilAgente",
                table: "RepasseRiscoHidrologicos",
                column: "IdPerfilAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepasseRiscoHidrologicos");
        }
    }
}

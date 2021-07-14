using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class Consumo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumoGeracaoPerfilAgentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumoMWh = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumoGeracaoPerfilAgentes", x => x.Id);
                    table.UniqueConstraint("AK_ConsumoGeracaoPerfilAgentes_Mes_IdPerfilAgente", x => new { x.Mes, x.IdPerfilAgente });
                    table.ForeignKey(
                        name: "FK_ConsumoGeracaoPerfilAgentes_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumoParcelaCargas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoCarga = table.Column<int>(type: "int", nullable: false),
                    Carga = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    RamoAtividade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Submercado = table.Column<int>(type: "int", nullable: false),
                    DataMigracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodigoPerfilDistribuidora = table.Column<int>(type: "int", nullable: true),
                    SiglaPerfilDistribuidora = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CapacidadeCargaMw = table.Column<double>(type: "float", nullable: false),
                    ConsumoAmbienteLivreMWh = table.Column<double>(type: "float", nullable: false),
                    ConsumoAjustadoParcelaCativaCargaLivreMWh = table.Column<double>(type: "float", nullable: false),
                    ConsumoAjustadoParcelaCargaMWh = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumoParcelaCargas", x => x.Id);
                    table.UniqueConstraint("AK_ConsumoParcelaCargas_Mes_IdPerfilAgente_CodigoCarga", x => new { x.Mes, x.IdPerfilAgente, x.CodigoCarga });
                    table.ForeignKey(
                        name: "FK_ConsumoParcelaCargas_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumoPerfilAgentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patamar = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consumo = table.Column<double>(type: "float", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumoPerfilAgentes", x => x.Id);
                    table.UniqueConstraint("AK_ConsumoPerfilAgentes_Mes_IdPerfilAgente_Patamar", x => new { x.Mes, x.IdPerfilAgente, x.Patamar });
                    table.ForeignKey(
                        name: "FK_ConsumoPerfilAgentes_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoGeracaoPerfilAgentes_IdPerfilAgente",
                table: "ConsumoGeracaoPerfilAgentes",
                column: "IdPerfilAgente");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoParcelaCargas_IdPerfilAgente",
                table: "ConsumoParcelaCargas",
                column: "IdPerfilAgente");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoPerfilAgentes_IdPerfilAgente",
                table: "ConsumoPerfilAgentes",
                column: "IdPerfilAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumoGeracaoPerfilAgentes");

            migrationBuilder.DropTable(
                name: "ConsumoParcelaCargas");

            migrationBuilder.DropTable(
                name: "ConsumoPerfilAgentes");
        }
    }
}

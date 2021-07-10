using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class AddContratos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    ContratacaoMWm = table.Column<double>(type: "float", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    IdPerfilAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.UniqueConstraint("AK_Contratos_Mes_Tipo", x => new { x.Mes, x.Tipo });
                    table.ForeignKey(
                        name: "FK_Contratos_PerfilAgentes_IdPerfilAgente",
                        column: x => x.IdPerfilAgente,
                        principalTable: "PerfilAgentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_IdPerfilAgente",
                table: "Contratos",
                column: "IdPerfilAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contratos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class AddAgentesAndPerfisAgentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeEmpresarial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agentes", x => x.Id);
                    table.UniqueConstraint("AK_Agentes_Cnpj", x => x.Cnpj);
                    table.UniqueConstraint("AK_Agentes_Codigo", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "PerfilAgentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Classe = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Submercado = table.Column<int>(type: "int", nullable: true),
                    Varejista = table.Column<bool>(type: "bit", nullable: false),
                    IdAgente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilAgentes", x => x.Id);
                    table.UniqueConstraint("AK_PerfilAgentes_Codigo", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_PerfilAgentes_Agentes_IdAgente",
                        column: x => x.IdAgente,
                        principalTable: "Agentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerfilAgentes_IdAgente",
                table: "PerfilAgentes",
                column: "IdAgente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerfilAgentes");

            migrationBuilder.DropTable(
                name: "Agentes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceitaFederal.Domain.Migrations
{
    public partial class TablesReceitaFederal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotivosSituacaoCadastral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DescricaoMotivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosSituacaoCadastral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaFederalArquivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaFederalArquivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NomeFantasia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SituacaoCadastral = table.Column<int>(type: "int", nullable: false),
                    MotivoSituacaoCadastralId = table.Column<int>(type: "int", nullable: true),
                    InicioAtividade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CapitalSocial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.UniqueConstraint("AK_Empresas_Cnpj", x => x.Cnpj);
                    table.ForeignKey(
                        name: "FK_Empresas_MotivosSituacaoCadastral_MotivoSituacaoCadastralId",
                        column: x => x.MotivoSituacaoCadastralId,
                        principalTable: "MotivosSituacaoCadastral",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    TipoSocio = table.Column<int>(type: "int", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                    table.UniqueConstraint("AK_Socios_Numero", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_Socios_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_MotivoSituacaoCadastralId",
                table: "Empresas",
                column: "MotivoSituacaoCadastralId");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_EmpresaId",
                table: "Socios",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceitaFederalArquivos");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "MotivosSituacaoCadastral");
        }
    }
}

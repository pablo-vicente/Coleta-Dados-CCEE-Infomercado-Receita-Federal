using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosGeracaoUsinas_ParcelaUsinas_idParcelaUsina",
                table: "DadosGeracaoUsinas");

            migrationBuilder.DropIndex(
                name: "IX_DadosGeracaoUsinas_idParcelaUsina",
                table: "DadosGeracaoUsinas");

            migrationBuilder.RenameColumn(
                name: "idParcelaUsina",
                table: "DadosGeracaoUsinas",
                newName: "IdParcelaUsina");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DadosGeracaoUsinas_IdParcelaUsina_IdPerfilAgente_Mes",
                table: "DadosGeracaoUsinas",
                columns: new[] { "IdParcelaUsina", "IdPerfilAgente", "Mes" });

            migrationBuilder.AddForeignKey(
                name: "FK_DadosGeracaoUsinas_ParcelaUsinas_IdParcelaUsina",
                table: "DadosGeracaoUsinas",
                column: "IdParcelaUsina",
                principalTable: "ParcelaUsinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosGeracaoUsinas_ParcelaUsinas_IdParcelaUsina",
                table: "DadosGeracaoUsinas");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DadosGeracaoUsinas_IdParcelaUsina_IdPerfilAgente_Mes",
                table: "DadosGeracaoUsinas");

            migrationBuilder.RenameColumn(
                name: "IdParcelaUsina",
                table: "DadosGeracaoUsinas",
                newName: "idParcelaUsina");

            migrationBuilder.CreateIndex(
                name: "IX_DadosGeracaoUsinas_idParcelaUsina",
                table: "DadosGeracaoUsinas",
                column: "idParcelaUsina");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosGeracaoUsinas_ParcelaUsinas_idParcelaUsina",
                table: "DadosGeracaoUsinas",
                column: "idParcelaUsina",
                principalTable: "ParcelaUsinas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

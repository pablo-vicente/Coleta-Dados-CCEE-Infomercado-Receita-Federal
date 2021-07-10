using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class FixAlternativeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contratos_Data_Tipo",
                table: "Contratos");

            migrationBuilder.DropIndex(
                name: "IX_Contratos_IdPerfilAgente",
                table: "Contratos");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contratos_IdPerfilAgente_Data_Tipo",
                table: "Contratos",
                columns: new[] { "IdPerfilAgente", "Data", "Tipo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contratos_IdPerfilAgente_Data_Tipo",
                table: "Contratos");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contratos_Data_Tipo",
                table: "Contratos",
                columns: new[] { "Data", "Tipo" });

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_IdPerfilAgente",
                table: "Contratos",
                column: "IdPerfilAgente");
        }
    }
}

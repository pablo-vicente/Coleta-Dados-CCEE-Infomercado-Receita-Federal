using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceitaFederal.Domain.Migrations
{
    public partial class FixAlternativeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Socios_Numero",
                table: "Socios");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Socios",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Socios",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Socios_Numero",
                table: "Socios",
                column: "Numero");
        }
    }
}

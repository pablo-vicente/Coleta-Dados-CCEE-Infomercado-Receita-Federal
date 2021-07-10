using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infomercado.Domain.Migrations
{
    public partial class ChangeColunMesToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contratos_Mes_Tipo",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Contratos");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Contratos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contratos_Data_Tipo",
                table: "Contratos",
                columns: new[] { "Data", "Tipo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Contratos_Data_Tipo",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Contratos");

            migrationBuilder.AddColumn<int>(
                name: "Mes",
                table: "Contratos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Contratos_Mes_Tipo",
                table: "Contratos",
                columns: new[] { "Mes", "Tipo" });
        }
    }
}

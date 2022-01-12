using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoFinalMVC.Migrations
{
    public partial class Teste1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Doutor_DoutorId",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Consulta");

            migrationBuilder.AlterColumn<int>(
                name: "DoutorId",
                table: "Consulta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Doutor_DoutorId",
                table: "Consulta",
                column: "DoutorId",
                principalTable: "Doutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Doutor_DoutorId",
                table: "Consulta");

            migrationBuilder.AlterColumn<int>(
                name: "DoutorId",
                table: "Consulta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Consulta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Doutor_DoutorId",
                table: "Consulta",
                column: "DoutorId",
                principalTable: "Doutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

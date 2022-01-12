using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoFinalMVC.Migrations
{
    public partial class EspecForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doutor_Especializacao_EspecializacaoId",
                table: "Doutor");

            migrationBuilder.AlterColumn<int>(
                name: "EspecializacaoId",
                table: "Doutor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doutor_Especializacao_EspecializacaoId",
                table: "Doutor",
                column: "EspecializacaoId",
                principalTable: "Especializacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doutor_Especializacao_EspecializacaoId",
                table: "Doutor");

            migrationBuilder.AlterColumn<int>(
                name: "EspecializacaoId",
                table: "Doutor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doutor_Especializacao_EspecializacaoId",
                table: "Doutor",
                column: "EspecializacaoId",
                principalTable: "Especializacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

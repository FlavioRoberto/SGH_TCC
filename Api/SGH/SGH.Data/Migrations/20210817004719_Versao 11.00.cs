using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao1100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Aula",
                table: "Aulas");

            migrationBuilder.AlterColumn<long>(
                name: "Aula_Sala",
                table: "Aulas",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Aula",
                table: "Aulas",
                column: "Aula_Sala",
                principalTable: "Sala",
                principalColumn: "Sala_Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Aula",
                table: "Aulas");

            migrationBuilder.AlterColumn<long>(
                name: "Aula_Sala",
                table: "Aulas",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Aula",
                table: "Aulas",
                column: "Aula_Sala",
                principalTable: "Sala",
                principalColumn: "Sala_Codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

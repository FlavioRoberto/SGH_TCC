using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao0600 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Usu_Curso",
                table: "Usuario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Usu_Curso",
                table: "Usuario",
                column: "Usu_Curso");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso",
                table: "Usuario",
                column: "Usu_Curso",
                principalTable: "Curso",
                principalColumn: "Curso_Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Usu_Curso",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Usu_Curso",
                table: "Usuario");
        }
    }
}

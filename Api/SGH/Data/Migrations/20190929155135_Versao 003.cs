using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "curric_periodo",
                table: "curriculo");

            migrationBuilder.AddColumn<int>(
                name: "curdis_periodo",
                table: "curriculo_disciplina",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "curdis_periodo",
                table: "curriculo_disciplina");

            migrationBuilder.AddColumn<int>(
                name: "curric_periodo",
                table: "curriculo",
                nullable: false,
                defaultValue: 0);
        }
    }
}

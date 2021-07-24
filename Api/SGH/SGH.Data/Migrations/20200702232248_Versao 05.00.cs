using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao0500 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Turno_Descricao",
                table: "Turno",
                newName: "Turno_Horarios");

            migrationBuilder.AddColumn<string>(
                name: "Horarios",
                table: "Turno",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horarios",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "Turno_Horarios",
                table: "Turno",
                newName: "Turno_Descricao");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao1300 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Usu_Email",
                table: "Usuario",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Prof_Email",
                table: "Professor",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Usu_Email",
                table: "Usuario",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Prof_Email",
                table: "Professor",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

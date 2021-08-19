using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao1200 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cargo_Edital",
                table: "Cargo",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cargo_Edital",
                table: "Cargo",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

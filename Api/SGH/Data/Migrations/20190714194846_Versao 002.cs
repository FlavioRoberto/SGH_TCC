using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario_Perfil",
                columns: table => new
                {
                    usuPrf_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    usuPrf_descricao = table.Column<string>(maxLength: 45, nullable: false),
                    usuPrf_administrador = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Perfil", x => x.usuPrf_codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario_Perfil");
        }
    }
}

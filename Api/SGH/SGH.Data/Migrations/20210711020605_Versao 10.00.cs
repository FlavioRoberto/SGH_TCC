using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Usuario_Perfil", new string[] { "UsuPrf_Descricao", "UsuPrf_Administrador" },  new object[] { "infraestrutura", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao_004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               insert into usuario (usu_nome, usu_login, usu_senha, usuPrf_Perfil, usu_email) 
		       values ('Administrador', 'admin','21232f297a57a5a743894a0e4a801fc3', 1,''); 
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {        }
    }
}

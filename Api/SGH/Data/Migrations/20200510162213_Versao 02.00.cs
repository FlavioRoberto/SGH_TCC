using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao0200 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "insert into \"Usuario_Perfil\" (\"UsuPrf_Descricao\", \"UsuPrf_Administrador\") values ('Administrador', 1);"+
                "insert into \"Usuario_Perfil\"(\"UsuPrf_Descricao\", \"UsuPrf_Administrador\") values('Coordenação Pedagógica', 1);"+
                "insert into \"Usuario_Perfil\"(\"UsuPrf_Descricao\", \"UsuPrf_Administrador\") values('Coordenação de Curso', 0);"+
                "insert into \"Usuario_Perfil\"(\"UsuPrf_Descricao\", \"UsuPrf_Administrador\") values('Professores', 0);"+
                "insert into \"Usuario_Perfil\"(\"UsuPrf_Descricao\", \"UsuPrf_Administrador\") values('Usuario', 0);"+

                "insert into \"Usuario\"(\"Usu_Nome\", \"Usu_Login\", \"Usu_Perfil\", \"Usu_Senha\", \"Usu_Email\")"+
                    "values('Administrador', 'admin', (select \"UsuPrf_Codigo\" from \"Usuario_Perfil\" where \"UsuPrf_Descricao\" = 'Administrador'), '21232f297a57a5a743894a0e4a801fc3','');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

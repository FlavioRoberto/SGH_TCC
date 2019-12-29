using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Administrador', true);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Coordenação Pedagógica', true);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Coordenação de Curso', false);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Professores', false);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Usuario', false);
                
                 insert into usuario (usuario.usu_nome, usuario.usu_login, usuario.usuPrf_Perfil, usuario.usu_senha, usuario.usu_email) 
	                   values ('Administrador','admin',(select usuPrf_codigo from usuario_perfil where usuPrf_descricao = 'Administrador'), '21232f297a57a5a743894a0e4a801fc3','');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

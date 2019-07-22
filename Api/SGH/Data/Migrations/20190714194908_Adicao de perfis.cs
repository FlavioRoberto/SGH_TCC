using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Adicaodeperfis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Administrador', true);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Coordenação Pedagógica', true);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Coordenação de Curso', false);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Professores', false);
                insert into usuario_perfil (usuPrf_descricao, usuPrf_administrador) values ('Usuario', false);
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

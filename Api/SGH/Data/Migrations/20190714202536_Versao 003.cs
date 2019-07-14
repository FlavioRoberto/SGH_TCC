using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    usu_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    usu_nome = table.Column<string>(maxLength: 45, nullable: false),
                    usu_telefone = table.Column<string>(maxLength: 12, nullable: true),
                    usu_login = table.Column<string>(maxLength: 30, nullable: false),
                    usu_senha = table.Column<string>(maxLength: 35, nullable: false),
                    usu_email = table.Column<string>(maxLength: 50, nullable: false),
                    usuPrf_Perfil = table.Column<int>(nullable: false),
                    usu_foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.usu_codigo);
                    table.ForeignKey(
                        name: "FK_Perfil",
                        column: x => x.usuPrf_Perfil,
                        principalTable: "Usuario_Perfil",
                        principalColumn: "usuPrf_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuario_usuPrf_Perfil",
                table: "usuario",
                column: "usuPrf_Perfil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}

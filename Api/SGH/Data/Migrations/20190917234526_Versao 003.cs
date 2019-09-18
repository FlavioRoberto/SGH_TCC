using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "professor",
                columns: table => new
                {
                    prof_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    prof_matricula = table.Column<string>(maxLength: 10, nullable: true),
                    prof_nome = table.Column<string>(maxLength: 45, nullable: false),
                    prof_telefone = table.Column<string>(maxLength: 12, nullable: true),
                    prof_email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor", x => x.prof_codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "professor");
        }
    }
}

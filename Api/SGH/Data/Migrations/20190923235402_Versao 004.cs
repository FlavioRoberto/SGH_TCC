using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "prof_ativo",
                table: "professor",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "professor_curso",
                columns: table => new
                {
                    profcur_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    profcur_professor = table.Column<int>(nullable: false),
                    profcur_curso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor_curso", x => x.profcur_codigo);
                    table.ForeignKey(
                        name: "FK_professor_curso_curso_profcur_curso",
                        column: x => x.profcur_curso,
                        principalTable: "curso",
                        principalColumn: "curso_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_professor_curso_professor_profcur_professor",
                        column: x => x.profcur_professor,
                        principalTable: "professor",
                        principalColumn: "prof_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_professor_curso_profcur_curso",
                table: "professor_curso",
                column: "profcur_curso");

            migrationBuilder.CreateIndex(
                name: "IX_professor_curso_profcur_professor",
                table: "professor_curso",
                column: "profcur_professor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "professor_curso");

            migrationBuilder.DropColumn(
                name: "prof_ativo",
                table: "professor");
        }
    }
}

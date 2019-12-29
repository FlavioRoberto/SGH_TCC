using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    cargo_numero = table.Column<int>(nullable: false),
                    cargo_edital = table.Column<int>(nullable: false),
                    cargo_ano = table.Column<int>(nullable: false),
                    cargo_semestre = table.Column<int>(nullable: false),
                    cargo_professor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Professor",
                        column: x => x.cargo_professor,
                        principalTable: "professor",
                        principalColumn: "prof_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargoDisciplina",
                columns: table => new
                {
                    cardis_disciplina = table.Column<int>(nullable: false),
                    cardis_cargo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoDisciplina", x => new { x.cardis_cargo, x.cardis_disciplina });
                    table.ForeignKey(
                        name: "FK_Cargo",
                        column: x => x.cardis_cargo,
                        principalTable: "Cargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cargo_Disciplina",
                        column: x => x.cardis_disciplina,
                        principalTable: "curriculo_disciplina",
                        principalColumn: "curdis_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_cargo_professor",
                table: "Cargo",
                column: "cargo_professor");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDisciplina_cardis_disciplina",
                table: "CargoDisciplina",
                column: "cardis_disciplina");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoDisciplina");

            migrationBuilder.DropTable(
                name: "Cargo");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao300 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoDisciplina");

            migrationBuilder.CreateTable(
                name: "cargo_disciplina",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    cardis_disciplina = table.Column<int>(nullable: false),
                    cardis_cargo = table.Column<int>(nullable: false),
                    cardis_turno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargo_disciplina", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Cargo",
                        column: x => x.cardis_cargo,
                        principalTable: "Cargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cargo_Disciplina",
                        column: x => x.cardis_disciplina,
                        principalTable: "curriculo_disciplina",
                        principalColumn: "curdis_codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_Turno",
                        column: x => x.cardis_turno,
                        principalTable: "turno",
                        principalColumn: "turno_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cargo_disciplina_cardis_cargo",
                table: "cargo_disciplina",
                column: "cardis_cargo");

            migrationBuilder.CreateIndex(
                name: "IX_cargo_disciplina_cardis_disciplina",
                table: "cargo_disciplina",
                column: "cardis_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_cargo_disciplina_cardis_turno",
                table: "cargo_disciplina",
                column: "cardis_turno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cargo_disciplina");

            migrationBuilder.CreateTable(
                name: "CargoDisciplina",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    cardis_cargo = table.Column<int>(nullable: false),
                    cardis_disciplina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoDisciplina", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Cargo",
                        column: x => x.cardis_cargo,
                        principalTable: "Cargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cargo_Disciplina",
                        column: x => x.cardis_disciplina,
                        principalTable: "curriculo_disciplina",
                        principalColumn: "curdis_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoDisciplina_cardis_cargo",
                table: "CargoDisciplina",
                column: "cardis_cargo");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDisciplina_cardis_disciplina",
                table: "CargoDisciplina",
                column: "cardis_disciplina");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao300 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "horarios_aula",
                columns: table => new
                {
                    horario_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    horario_ano = table.Column<int>(nullable: false),
                    horario_semestre = table.Column<int>(nullable: false),
                    horario_periodo = table.Column<int>(nullable: false),
                    horario_turno = table.Column<int>(nullable: false),
                    horario_curriculo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios_aula", x => x.horario_codigo);
                    table.ForeignKey(
                        name: "FK_Curriculo_Horario",
                        column: x => x.horario_curriculo,
                        principalTable: "curriculo",
                        principalColumn: "curric_codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turno_Horario",
                        column: x => x.horario_turno,
                        principalTable: "turno",
                        principalColumn: "turno_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_horarios_aula_horario_curriculo",
                table: "horarios_aula",
                column: "horario_curriculo");

            migrationBuilder.CreateIndex(
                name: "IX_horarios_aula_horario_turno",
                table: "horarios_aula",
                column: "horario_turno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "horarios_aula");
        }
    }
}

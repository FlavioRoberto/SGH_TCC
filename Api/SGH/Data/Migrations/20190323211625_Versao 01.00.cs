using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao0100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    curso_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    curso_descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curso", x => x.curso_codigo);
                });

            migrationBuilder.CreateTable(
                name: "disciplina_tipo",
                columns: table => new
                {
                    distip_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    distip_descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplina_tipo", x => x.distip_codigo);
                });

            migrationBuilder.CreateTable(
                name: "turno",
                columns: table => new
                {
                    turno_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    turno_descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turno", x => x.turno_codigo);
                });

            migrationBuilder.CreateTable(
                name: "disciplina",
                columns: table => new
                {
                    dis_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    dis_descricao = table.Column<string>(nullable: true),
                    dis_tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplina", x => x.dis_codigo);
                    table.ForeignKey(
                        name: "FK_disciplina_disciplina_tipo_dis_tipo",
                        column: x => x.dis_tipo,
                        principalTable: "disciplina_tipo",
                        principalColumn: "distip_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "curriculo",
                columns: table => new
                {
                    curric_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    curric_periodo = table.Column<int>(nullable: false),
                    curric_curso = table.Column<int>(nullable: false),
                    curric_turno = table.Column<int>(nullable: false),
                    curric_ano = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo", x => x.curric_codigo);
                    table.ForeignKey(
                        name: "FK_curriculo_curso_curric_curso",
                        column: x => x.curric_curso,
                        principalTable: "curso",
                        principalColumn: "curso_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_curriculo_turno_curric_turno",
                        column: x => x.curric_turno,
                        principalTable: "turno",
                        principalColumn: "turno_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "curriculo_disciplina",
                columns: table => new
                {
                    curdis_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    curdis_disciplina = table.Column<int>(nullable: false),
                    curdis_curriculo = table.Column<int>(nullable: false),
                    curdis_carga_horaria_semanal_teoricoa = table.Column<int>(nullable: false),
                    curdis_carga_horaria_semanal_pratica = table.Column<int>(nullable: false),
                    CargaHorariaSemanalTotal = table.Column<int>(nullable: false),
                    curdis_hora_aula_total = table.Column<int>(nullable: false),
                    curdis_hora_total = table.Column<int>(nullable: false),
                    curdis_credito = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo_disciplina", x => x.curdis_codigo);
                    table.ForeignKey(
                        name: "FK_curriculo_disciplina_curriculo_curdis_curriculo",
                        column: x => x.curdis_curriculo,
                        principalTable: "curriculo",
                        principalColumn: "curric_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_curriculo_disciplina_disciplina_curdis_disciplina",
                        column: x => x.curdis_disciplina,
                        principalTable: "disciplina",
                        principalColumn: "dis_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "curriculo_disciplina_prerequisito",
                columns: table => new
                {
                    curpre_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    curpre_disciplina = table.Column<int>(nullable: false),
                    curpre_curriculo_disciplina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo_disciplina_prerequisito", x => x.curpre_codigo);
                    table.ForeignKey(
                        name: "FK_curriculo_disciplina",
                        column: x => x.curpre_curriculo_disciplina,
                        principalTable: "curriculo_disciplina",
                        principalColumn: "curdis_codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disciplina",
                        column: x => x.curpre_disciplina,
                        principalTable: "disciplina",
                        principalColumn: "dis_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_curric_curso",
                table: "curriculo",
                column: "curric_curso");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_curric_turno",
                table: "curriculo",
                column: "curric_turno");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_curdis_curriculo",
                table: "curriculo_disciplina",
                column: "curdis_curriculo");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_curdis_disciplina",
                table: "curriculo_disciplina",
                column: "curdis_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_prerequisito_curpre_curriculo_disciplina",
                table: "curriculo_disciplina_prerequisito",
                column: "curpre_curriculo_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_prerequisito_curpre_disciplina",
                table: "curriculo_disciplina_prerequisito",
                column: "curpre_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_dis_tipo",
                table: "disciplina",
                column: "dis_tipo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "curriculo_disciplina_prerequisito");

            migrationBuilder.DropTable(
                name: "curriculo_disciplina");

            migrationBuilder.DropTable(
                name: "curriculo");

            migrationBuilder.DropTable(
                name: "disciplina");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "turno");

            migrationBuilder.DropTable(
                name: "disciplina_tipo");
        }
    }
}

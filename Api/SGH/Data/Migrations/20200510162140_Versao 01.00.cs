using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SGH.Data.Migrations
{
    public partial class Versao0100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bloco",
                columns: table => new
                {
                    Bloco_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bloco_Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloco", x => x.Bloco_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Curso_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Curso_Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Curso_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina_Tipo",
                columns: table => new
                {
                    Distip_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Distip_Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina_Tipo", x => x.Distip_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Prof_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Prof_Matricula = table.Column<string>(maxLength: 10, nullable: true),
                    Prof_Nome = table.Column<string>(maxLength: 45, nullable: false),
                    Prof_Telefone = table.Column<string>(maxLength: 12, nullable: true),
                    Prof_Email = table.Column<string>(maxLength: 50, nullable: false),
                    Prof_Ativo = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Prof_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Turno_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Turno_Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Turno_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Perfil",
                columns: table => new
                {
                    UsuPrf_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UsuPrf_Descricao = table.Column<string>(maxLength: 45, nullable: false),
                    UsuPrf_Administrador = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Perfil", x => x.UsuPrf_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Sala_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Sala_Numero = table.Column<int>(nullable: false),
                    Sala_Descricao = table.Column<string>(nullable: true),
                    Sala_Laboratorio = table.Column<int>(nullable: false),
                    Sala_Bloco = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Sala_Codigo);
                    table.ForeignKey(
                        name: "FK_Sala_Bloco_Sala_Bloco",
                        column: x => x.Sala_Bloco,
                        principalTable: "Bloco",
                        principalColumn: "Bloco_Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curriculo",
                columns: table => new
                {
                    Curric_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Curric_Curso = table.Column<int>(nullable: false),
                    Curric_Ano = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo", x => x.Curric_Codigo);
                    table.ForeignKey(
                        name: "FK_Curriculo_Curso_Curric_Curso",
                        column: x => x.Curric_Curso,
                        principalTable: "Curso",
                        principalColumn: "Curso_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Dis_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Dis_Descricao = table.Column<string>(nullable: true),
                    Dis_Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Dis_Codigo);
                    table.ForeignKey(
                        name: "FK_Disciplina_Disciplina_Tipo_Dis_Tipo",
                        column: x => x.Dis_Tipo,
                        principalTable: "Disciplina_Tipo",
                        principalColumn: "Distip_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cargo_Mumero = table.Column<int>(nullable: false),
                    Cargo_Edital = table.Column<int>(nullable: false),
                    Cargo_Ano = table.Column<int>(nullable: false),
                    Cargo_Semestre = table.Column<int>(nullable: false),
                    Cargo_Professor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Professor",
                        column: x => x.Cargo_Professor,
                        principalTable: "Professor",
                        principalColumn: "Prof_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Usu_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Usu_Nome = table.Column<string>(maxLength: 45, nullable: false),
                    Usu_Telefone = table.Column<string>(maxLength: 12, nullable: true),
                    Usu_Login = table.Column<string>(maxLength: 30, nullable: false),
                    Usu_Senha = table.Column<string>(maxLength: 35, nullable: false),
                    Usu_Email = table.Column<string>(maxLength: 50, nullable: false),
                    Usu_Perfil = table.Column<int>(nullable: false),
                    Usu_Foto = table.Column<string>(nullable: true),
                    Usu_Ativo = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Usu_Codigo);
                    table.ForeignKey(
                        name: "FK_Perfil",
                        column: x => x.Usu_Perfil,
                        principalTable: "Usuario_Perfil",
                        principalColumn: "UsuPrf_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Horarios_Aula",
                columns: table => new
                {
                    Horario_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Horario_Ano = table.Column<int>(nullable: false),
                    Horario_Semestre = table.Column<int>(nullable: false),
                    Horario_Periodo = table.Column<int>(nullable: false),
                    Horario_Turno = table.Column<int>(nullable: false),
                    Horario_Curriculo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios_Aula", x => x.Horario_Codigo);
                    table.ForeignKey(
                        name: "FK_Curriculo_Horario",
                        column: x => x.Horario_Curriculo,
                        principalTable: "Curriculo",
                        principalColumn: "Curric_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turno_Horario",
                        column: x => x.Horario_Turno,
                        principalTable: "Turno",
                        principalColumn: "Turno_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculo_Disciplina",
                columns: table => new
                {
                    Curdis_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Curdis_Disciplina = table.Column<int>(nullable: true),
                    Curdis_Curriculo = table.Column<int>(nullable: false),
                    Curdis_Periodo = table.Column<int>(nullable: false),
                    Curdis_Quantidade_Aulas_Semanais_Teorica = table.Column<int>(nullable: false),
                    Curdis_Quantidade_Aulas_Semanal_Pratica = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo_Disciplina", x => x.Curdis_Codigo);
                    table.ForeignKey(
                        name: "FK_Curriculo",
                        column: x => x.Curdis_Curriculo,
                        principalTable: "Curriculo",
                        principalColumn: "Curric_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplina",
                        column: x => x.Curdis_Disciplina,
                        principalTable: "Disciplina",
                        principalColumn: "Dis_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cargo_Disciplina",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cardis_Disciplina = table.Column<int>(nullable: false),
                    Cardis_Cargo = table.Column<int>(nullable: false),
                    Cardis_Turno = table.Column<int>(nullable: false),
                    Cardis_Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo_Disciplina", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Cargo",
                        column: x => x.Cardis_Cargo,
                        principalTable: "Cargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cargo_Disciplina",
                        column: x => x.Cardis_Disciplina,
                        principalTable: "Curriculo_Disciplina",
                        principalColumn: "Curdis_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_Turno",
                        column: x => x.Cardis_Turno,
                        principalTable: "Turno",
                        principalColumn: "Turno_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculo_Disciplina_Pre_Requisito",
                columns: table => new
                {
                    DisPre_Curriculo_Disciplina = table.Column<int>(nullable: false),
                    DisPre_Disciplina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo_Disciplina_Pre_Requisito", x => new { x.DisPre_Curriculo_Disciplina, x.DisPre_Disciplina });
                    table.ForeignKey(
                        name: "FK_Curriculo_Disciplina",
                        column: x => x.DisPre_Curriculo_Disciplina,
                        principalTable: "Curriculo_Disciplina",
                        principalColumn: "Curdis_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Curriculo_Disciplina_Pre_Req",
                        column: x => x.DisPre_Disciplina,
                        principalTable: "Disciplina",
                        principalColumn: "Dis_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Aula_Codigo = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aula_Horarario = table.Column<int>(nullable: false),
                    Aula_Disciplina = table.Column<int>(nullable: false),
                    Aula_Sala = table.Column<int>(nullable: false),
                    Aula_Laboratorio = table.Column<int>(nullable: false),
                    Aula_Desdobramento = table.Column<int>(nullable: false),
                    Aula_Descricao_Desdobramento = table.Column<string>(nullable: true),
                    Aula_Dia_Semana = table.Column<string>(nullable: true),
                    Aula_Hora = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Aula_Codigo);
                    table.ForeignKey(
                        name: "FK_Cargo_Disciplina_Aula",
                        column: x => x.Aula_Disciplina,
                        principalTable: "Cargo_Disciplina",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Horario_Aula",
                        column: x => x.Aula_Horarario,
                        principalTable: "Horarios_Aula",
                        principalColumn: "Horario_Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sala_Aula",
                        column: x => x.Aula_Sala,
                        principalTable: "Sala",
                        principalColumn: "Sala_Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Aula_Disciplina",
                table: "Aulas",
                column: "Aula_Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Aula_Horarario",
                table: "Aulas",
                column: "Aula_Horarario");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Aula_Sala",
                table: "Aulas",
                column: "Aula_Sala");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_Cargo_Professor",
                table: "Cargo",
                column: "Cargo_Professor");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_Disciplina_Cardis_Cargo",
                table: "Cargo_Disciplina",
                column: "Cardis_Cargo");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_Disciplina_Cardis_Disciplina",
                table: "Cargo_Disciplina",
                column: "Cardis_Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_Disciplina_Cardis_Turno",
                table: "Cargo_Disciplina",
                column: "Cardis_Turno");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_Curric_Curso",
                table: "Curriculo",
                column: "Curric_Curso");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_Disciplina_Curdis_Curriculo",
                table: "Curriculo_Disciplina",
                column: "Curdis_Curriculo");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_Disciplina_Curdis_Disciplina",
                table: "Curriculo_Disciplina",
                column: "Curdis_Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_Disciplina_Pre_Requisito_DisPre_Disciplina",
                table: "Curriculo_Disciplina_Pre_Requisito",
                column: "DisPre_Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_Dis_Tipo",
                table: "Disciplina",
                column: "Dis_Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_Aula_Horario_Curriculo",
                table: "Horarios_Aula",
                column: "Horario_Curriculo");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_Aula_Horario_Turno",
                table: "Horarios_Aula",
                column: "Horario_Turno");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_Sala_Bloco",
                table: "Sala",
                column: "Sala_Bloco");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Usu_Perfil",
                table: "Usuario",
                column: "Usu_Perfil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Curriculo_Disciplina_Pre_Requisito");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cargo_Disciplina");

            migrationBuilder.DropTable(
                name: "Horarios_Aula");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Usuario_Perfil");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Curriculo_Disciplina");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Bloco");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Curriculo");

            migrationBuilder.DropTable(
                name: "Disciplina");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Disciplina_Tipo");
        }
    }
}

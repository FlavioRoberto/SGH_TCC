﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao100 : Migration
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
                name: "professor",
                columns: table => new
                {
                    prof_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    prof_matricula = table.Column<string>(maxLength: 10, nullable: true),
                    prof_nome = table.Column<string>(maxLength: 45, nullable: false),
                    prof_telefone = table.Column<string>(maxLength: 12, nullable: true),
                    prof_email = table.Column<string>(maxLength: 50, nullable: false),
                    prof_ativo = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor", x => x.prof_codigo);
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
                name: "Usuario_Perfil",
                columns: table => new
                {
                    usuPrf_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    usuPrf_descricao = table.Column<string>(maxLength: 45, nullable: false),
                    usuPrf_administrador = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Perfil", x => x.usuPrf_codigo);
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
                        onDelete: ReferentialAction.Restrict);
                });

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
                    cargo_professor = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Professor",
                        column: x => x.cargo_professor,
                        principalTable: "professor",
                        principalColumn: "prof_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "curriculo",
                columns: table => new
                {
                    curric_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    curric_curso = table.Column<int>(nullable: false),
                    curric_ano = table.Column<int>(nullable: false),
                    TurnoCodigo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo", x => x.curric_codigo);
                    table.ForeignKey(
                        name: "FK_curriculo_curso_curric_curso",
                        column: x => x.curric_curso,
                        principalTable: "curso",
                        principalColumn: "curso_codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_curriculo_turno_TurnoCodigo",
                        column: x => x.TurnoCodigo,
                        principalTable: "turno",
                        principalColumn: "turno_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    usu_foto = table.Column<string>(nullable: true),
                    usu_ativo = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.usu_codigo);
                    table.ForeignKey(
                        name: "FK_Perfil",
                        column: x => x.usuPrf_Perfil,
                        principalTable: "Usuario_Perfil",
                        principalColumn: "usuPrf_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "curriculo_disciplina",
                columns: table => new
                {
                    curdis_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    curdis_disciplina = table.Column<int>(nullable: true),
                    curdis_curriculo = table.Column<int>(nullable: false),
                    curdis_periodo = table.Column<int>(nullable: false),
                    curdis_quantidade_aulas_semanais_teorica = table.Column<int>(nullable: false),
                    curdis_quantidade_aulas_semanal_pratica = table.Column<int>(nullable: false),
                    curdis_credito = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo_disciplina", x => x.curdis_codigo);
                    table.ForeignKey(
                        name: "FK_Curriculo",
                        column: x => x.curdis_curriculo,
                        principalTable: "curriculo",
                        principalColumn: "curric_codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplina",
                        column: x => x.curdis_disciplina,
                        principalTable: "disciplina",
                        principalColumn: "dis_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CargoDisciplina",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    cardis_disciplina = table.Column<int>(nullable: false),
                    cardis_cargo = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "curriculo_disciplina_pre_requisito",
                columns: table => new
                {
                    disPre_curriculo_disciplina = table.Column<int>(nullable: false),
                    disPre_disciplina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo_disciplina_pre_requisito", x => new { x.disPre_curriculo_disciplina, x.disPre_disciplina });
                    table.ForeignKey(
                        name: "FK_Curriculo_Disciplina",
                        column: x => x.disPre_curriculo_disciplina,
                        principalTable: "curriculo_disciplina",
                        principalColumn: "curdis_codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Curriculo_Disciplina_Pre_Req",
                        column: x => x.disPre_disciplina,
                        principalTable: "disciplina",
                        principalColumn: "dis_codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_cargo_professor",
                table: "Cargo",
                column: "cargo_professor");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDisciplina_cardis_cargo",
                table: "CargoDisciplina",
                column: "cardis_cargo");

            migrationBuilder.CreateIndex(
                name: "IX_CargoDisciplina_cardis_disciplina",
                table: "CargoDisciplina",
                column: "cardis_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_curric_curso",
                table: "curriculo",
                column: "curric_curso");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_TurnoCodigo",
                table: "curriculo",
                column: "TurnoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_curdis_curriculo",
                table: "curriculo_disciplina",
                column: "curdis_curriculo");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_curdis_disciplina",
                table: "curriculo_disciplina",
                column: "curdis_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_pre_requisito_disPre_disciplina",
                table: "curriculo_disciplina_pre_requisito",
                column: "disPre_disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_dis_tipo",
                table: "disciplina",
                column: "dis_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_usuPrf_Perfil",
                table: "usuario",
                column: "usuPrf_Perfil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoDisciplina");

            migrationBuilder.DropTable(
                name: "curriculo_disciplina_pre_requisito");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "curriculo_disciplina");

            migrationBuilder.DropTable(
                name: "Usuario_Perfil");

            migrationBuilder.DropTable(
                name: "professor");

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
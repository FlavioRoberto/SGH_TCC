using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SGH.Data.Migrations
{
    public partial class Versao1400 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AulaDisciplinaAuxiliar",
                columns: table => new
                {
                    AulaDiscAux_Codigo = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AulaDiscAux_Aula = table.Column<long>(nullable: false),
                    AulaDiscAux_Disciplina = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AulaDisciplinaAuxiliar", x => x.AulaDiscAux_Codigo);
                    table.ForeignKey(
                        name: "FK_Aula",
                        column: x => x.AulaDiscAux_Aula,
                        principalTable: "Aulas",
                        principalColumn: "Aula_Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplina",
                        column: x => x.AulaDiscAux_Disciplina,
                        principalTable: "Cargo_Disciplina",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AulaDisciplinaAuxiliar_AulaDiscAux_Aula",
                table: "AulaDisciplinaAuxiliar",
                column: "AulaDiscAux_Aula");

            migrationBuilder.CreateIndex(
                name: "IX_AulaDisciplinaAuxiliar_AulaDiscAux_Disciplina",
                table: "AulaDisciplinaAuxiliar",
                column: "AulaDiscAux_Disciplina");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AulaDisciplinaAuxiliar");
        }
    }
}

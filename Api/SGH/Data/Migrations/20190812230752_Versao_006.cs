using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Versao_006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargaHorariaSemanalTotal",
                table: "curriculo_disciplina");

            migrationBuilder.DropColumn(
                name: "curdis_pre_requisito",
                table: "curriculo_disciplina");

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curriculo_Disciplina_Pre_Req",
                        column: x => x.disPre_disciplina,
                        principalTable: "disciplina",
                        principalColumn: "dis_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_disciplina_pre_requisito_disPre_disciplina",
                table: "curriculo_disciplina_pre_requisito",
                column: "disPre_disciplina");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "curriculo_disciplina_pre_requisito");

            migrationBuilder.AddColumn<int>(
                name: "CargaHorariaSemanalTotal",
                table: "curriculo_disciplina",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "curdis_pre_requisito",
                table: "curriculo_disciplina",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}

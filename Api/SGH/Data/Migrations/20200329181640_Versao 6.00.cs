using Microsoft.EntityFrameworkCore.Migrations;

namespace SGH.Data.Migrations
{
    public partial class Versao600 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_curriculo_turno_TurnoCodigo",
                table: "curriculo");

            migrationBuilder.DropIndex(
                name: "IX_curriculo_TurnoCodigo",
                table: "curriculo");

            migrationBuilder.DropColumn(
                name: "TurnoCodigo",
                table: "curriculo");

            migrationBuilder.CreateTable(
                name: "bloco",
                columns: table => new
                {
                    bloco_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    bloco_descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bloco", x => x.bloco_codigo);
                });

            migrationBuilder.CreateTable(
                name: "sala",
                columns: table => new
                {
                    sala_codigo = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    sala_numero = table.Column<int>(nullable: false),
                    sala_descricao = table.Column<string>(nullable: true),
                    sala_laboratorio = table.Column<int>(nullable: false),
                    sala_bloco = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sala", x => x.sala_codigo);
                    table.ForeignKey(
                        name: "FK_sala_bloco_sala_bloco",
                        column: x => x.sala_bloco,
                        principalTable: "bloco",
                        principalColumn: "bloco_codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sala_sala_bloco",
                table: "sala",
                column: "sala_bloco");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sala");

            migrationBuilder.DropTable(
                name: "bloco");

            migrationBuilder.AddColumn<int>(
                name: "TurnoCodigo",
                table: "curriculo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_TurnoCodigo",
                table: "curriculo",
                column: "TurnoCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_curriculo_turno_TurnoCodigo",
                table: "curriculo",
                column: "TurnoCodigo",
                principalTable: "turno",
                principalColumn: "turno_codigo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

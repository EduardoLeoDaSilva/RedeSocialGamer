using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class ClasseLikeAdicionadaAjeitando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    LikeId = table.Column<int>(nullable: false),
                    PostagemId = table.Column<int>(nullable: false),
                    UsuarioAutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.LikeId, x.PostagemId, x.UsuarioAutorId });
                    table.ForeignKey(
                        name: "FK_Like_Postagens_PostagemId",
                        column: x => x.PostagemId,
                        principalTable: "Postagens",
                        principalColumn: "PostagemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Usuarios_UsuarioAutorId",
                        column: x => x.UsuarioAutorId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostagemId",
                table: "Like",
                column: "PostagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UsuarioAutorId",
                table: "Like",
                column: "UsuarioAutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}

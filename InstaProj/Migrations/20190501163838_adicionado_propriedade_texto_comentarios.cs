using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class adicionado_propriedade_texto_comentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComentarioTextoUsuarioId",
                table: "Comentario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_ComentarioTextoUsuarioId",
                table: "Comentario",
                column: "ComentarioTextoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Usuarios_ComentarioTextoUsuarioId",
                table: "Comentario",
                column: "ComentarioTextoUsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Usuarios_ComentarioTextoUsuarioId",
                table: "Comentario");

            migrationBuilder.DropIndex(
                name: "IX_Comentario_ComentarioTextoUsuarioId",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "ComentarioTextoUsuarioId",
                table: "Comentario");
        }
    }
}

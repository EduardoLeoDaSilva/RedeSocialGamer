using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class AdicionandoPropriedadeAmgiostUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioId1",
                table: "Usuarios",
                column: "Amigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_UsuarioId1",
                table: "Usuarios",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Usuarios_UsuarioId1",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_UsuarioId1",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Usuarios");
        }
    }
}

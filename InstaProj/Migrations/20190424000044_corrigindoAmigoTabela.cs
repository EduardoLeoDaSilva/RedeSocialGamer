using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class corrigindoAmigoTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Amigo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amigo_UsuarioId",
                table: "Amigo",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amigo_Usuarios_UsuarioId",
                table: "Amigo",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amigo_Usuarios_UsuarioId",
                table: "Amigo");

            migrationBuilder.DropIndex(
                name: "IX_Amigo_UsuarioId",
                table: "Amigo");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Amigo");
        }
    }
}

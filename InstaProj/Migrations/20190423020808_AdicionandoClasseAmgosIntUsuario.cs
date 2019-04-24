using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class AdicionandoClasseAmgosIntUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Amigo",
                columns: table => new
                {
                    AmigoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioAmigoUsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigo", x => x.AmigoId);
                    table.ForeignKey(
                        name: "FK_Amigo_Usuarios_UsuarioAmigoUsuarioId",
                        column: x => x.UsuarioAmigoUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amigo_UsuarioAmigoUsuarioId",
                table: "Amigo",
                column: "UsuarioAmigoUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amigo");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioId1",
                table: "Usuarios",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Usuarios_UsuarioId1",
                table: "Usuarios",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

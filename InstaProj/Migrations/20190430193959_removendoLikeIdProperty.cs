using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class removendoLikeIdProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_PostagemId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Like");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "PostagemId", "UsuarioAutorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "Like",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                columns: new[] { "LikeId", "PostagemId", "UsuarioAutorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostagemId",
                table: "Like",
                column: "PostagemId");
        }
    }
}

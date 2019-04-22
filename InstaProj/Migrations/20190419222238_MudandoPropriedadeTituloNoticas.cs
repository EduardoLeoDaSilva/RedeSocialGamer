using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class MudandoPropriedadeTituloNoticas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imagem",
                table: "Postagens",
                newName: "Imagem");

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Postagens",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Noticias",
                unicode: false,
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Postagens");

            migrationBuilder.RenameColumn(
                name: "Imagem",
                table: "Postagens",
                newName: "imagem");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Noticias",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}

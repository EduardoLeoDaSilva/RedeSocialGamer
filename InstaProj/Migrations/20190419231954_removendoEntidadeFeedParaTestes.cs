using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaProj.Migrations
{
    public partial class removendoEntidadeFeedParaTestes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_FeedId",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Postagens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedId",
                table: "Postagens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    FeedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.FeedId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_FeedId",
                table: "Postagens",
                column: "FeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "FeedId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

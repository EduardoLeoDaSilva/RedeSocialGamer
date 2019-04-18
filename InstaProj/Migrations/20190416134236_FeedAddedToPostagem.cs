using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstaProj.Migrations
{
    public partial class FeedAddedToPostagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens");

            migrationBuilder.AlterColumn<int>(
                name: "FeedId",
                table: "Postagens",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "FeedId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens");

            migrationBuilder.AlterColumn<int>(
                name: "FeedId",
                table: "Postagens",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "FeedId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

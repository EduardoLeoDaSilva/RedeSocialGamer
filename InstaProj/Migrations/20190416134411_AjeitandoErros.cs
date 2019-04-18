using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstaProj.Migrations
{
    public partial class AjeitandoErros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Usuarios_UsuarioId",
                table: "Postagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Postagens",
                table: "Postagens");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_UsuarioId",
                table: "Postagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feeds",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Postagens");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Postagens",
                newName: "Postagem");

            migrationBuilder.RenameTable(
                name: "Feeds",
                newName: "Feed");

            migrationBuilder.RenameIndex(
                name: "IX_Postagens_FeedId",
                table: "Postagem",
                newName: "IX_Postagem_FeedId");

            migrationBuilder.AddColumn<int>(
                name: "PostagemId",
                table: "Usuario",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postagem",
                table: "Postagem",
                column: "PostagemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feed",
                table: "Feed",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PostagemId",
                table: "Usuario",
                column: "PostagemId",
                unique: true,
                filter: "[PostagemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagem_Feed_FeedId",
                table: "Postagem",
                column: "FeedId",
                principalTable: "Feed",
                principalColumn: "FeedId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Postagem_PostagemId",
                table: "Usuario",
                column: "PostagemId",
                principalTable: "Postagem",
                principalColumn: "PostagemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagem_Feed_FeedId",
                table: "Postagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Postagem_PostagemId",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_PostagemId",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Postagem",
                table: "Postagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feed",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "PostagemId",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Postagem",
                newName: "Postagens");

            migrationBuilder.RenameTable(
                name: "Feed",
                newName: "Feeds");

            migrationBuilder.RenameIndex(
                name: "IX_Postagem_FeedId",
                table: "Postagens",
                newName: "IX_Postagens_FeedId");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Postagens",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postagens",
                table: "Postagens",
                column: "PostagemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feeds",
                table: "Feeds",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_UsuarioId",
                table: "Postagens",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Feeds_FeedId",
                table: "Postagens",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "FeedId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Usuarios_UsuarioId",
                table: "Postagens",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

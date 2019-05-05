﻿// <auto-generated />
using System;
using InstaProj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InstaProj.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190429180050_ClasseLikeAdicionada")]
    partial class ClasseLikeAdicionada
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InstaProj.Models.Entidades.Amigo", b =>
                {
                    b.Property<int>("AmigoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UsuarioAmigoUsuarioId");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("AmigoId");

                    b.HasIndex("UsuarioAmigoUsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Amigo");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Comentario", b =>
                {
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PostagemId");

                    b.Property<int?>("UsuarioAutorUsuarioId");

                    b.HasKey("ComentarioId");

                    b.HasIndex("PostagemId");

                    b.HasIndex("UsuarioAutorUsuarioId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Imagem", b =>
                {
                    b.Property<int>("ImagemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PostagemId");

                    b.Property<byte[]>("foto");

                    b.HasKey("ImagemId");

                    b.HasIndex("PostagemId");

                    b.ToTable("Imagem");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Noticias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Titulo")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("texto");

                    b.HasKey("Id");

                    b.ToTable("Noticias");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Postagem", b =>
                {
                    b.Property<int>("PostagemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Texto");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("PostagemId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Postagens");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<string>("Sexo");

                    b.Property<byte[]>("foto");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Amigo", b =>
                {
                    b.HasOne("InstaProj.Models.Entidades.Usuario", "UsuarioAmigo")
                        .WithMany()
                        .HasForeignKey("UsuarioAmigoUsuarioId");

                    b.HasOne("InstaProj.Models.Entidades.Usuario", "Usuario")
                        .WithMany("Amigos")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Comentario", b =>
                {
                    b.HasOne("InstaProj.Models.Entidades.Postagem", "Postagem")
                        .WithMany("Comentarios")
                        .HasForeignKey("PostagemId");

                    b.HasOne("InstaProj.Models.Entidades.Usuario", "UsuarioAutor")
                        .WithMany()
                        .HasForeignKey("UsuarioAutorUsuarioId");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Imagem", b =>
                {
                    b.HasOne("InstaProj.Models.Entidades.Postagem", "Postagem")
                        .WithMany("Imagens")
                        .HasForeignKey("PostagemId");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Postagem", b =>
                {
                    b.HasOne("InstaProj.Models.Entidades.Usuario", "Usuario")
                        .WithMany("Postagens")
                        .HasForeignKey("UsuarioId");
                });
#pragma warning restore 612, 618
        }
    }
}

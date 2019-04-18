﻿// <auto-generated />
using InstaProj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace InstaProj.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190416134236_FeedAddedToPostagem")]
    partial class FeedAddedToPostagem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InstaProj.Models.Entidades.Feed", b =>
                {
                    b.Property<int>("FeedId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("FeedId");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Postagem", b =>
                {
                    b.Property<int>("PostagemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FeedId")
                        .IsRequired();

                    b.Property<int?>("UsuarioId");

                    b.HasKey("PostagemId");

                    b.HasIndex("FeedId");

                    b.HasIndex("UsuarioId")
                        .IsUnique()
                        .HasFilter("[UsuarioId] IS NOT NULL");

                    b.ToTable("Postagens");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<string>("Sexo");

                    b.Property<byte[]>("foto");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("InstaProj.Models.Entidades.Postagem", b =>
                {
                    b.HasOne("InstaProj.Models.Entidades.Feed", "Feed")
                        .WithMany("Postagens")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InstaProj.Models.Entidades.Usuario", "Usuario")
                        .WithOne("Postagem")
                        .HasForeignKey("InstaProj.Models.Entidades.Postagem", "UsuarioId");
                });
#pragma warning restore 612, 618
        }
    }
}

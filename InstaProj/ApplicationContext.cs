using InstaProj.Controllers.ControllersTempoReal;
using InstaProj.Models;
using InstaProj.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        //public DbSet<Feed> Feeds { get; set; }
        public DbSet<Noticias> Noticias { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioId);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Usuario>().HasMany(u => u.Amigos).WithOne(a => a.Usuario);

            modelBuilder.Entity<Postagem>().HasKey(p => p.PostagemId);
            modelBuilder.Entity<Postagem>().HasOne(p => p.Usuario).WithMany(u => u.Postagens);
            modelBuilder.Entity<Noticias>().Property("Titulo").HasMaxLength(25).IsUnicode(false);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Postagens).WithOne(p => p.Usuario);
            modelBuilder.Entity<Postagem>().HasMany(p => p.Imagens).WithOne(i => i.Postagem);
            modelBuilder.Entity<Postagem>().HasMany(p => p.Comentarios).WithOne(c => c.Postagem);
            modelBuilder.Entity<Comentario>().HasOne(p => p.UsuarioAutor);
            modelBuilder.Entity<Comentario>().HasOne(p => p.Postagem);

            modelBuilder.Entity<Comentario>().HasKey(c => c.ComentarioId);
            modelBuilder.Entity<Like>().HasKey(l => new { l.PostagemId,l.UsuarioAutorId });
            modelBuilder.Entity<Like>().HasOne(p => p.UsuarioAutor);
            modelBuilder.Entity<Like>().HasOne(p => p.Postagem);

            modelBuilder.Entity<UsuarioLogado>().HasKey(p => p.UsuarioLogadoId);
            //modelBuilder.Entity<Feed>().HasKey(f => f.FeedId);
            //modelBuilder.Entity<Feed>().HasMany(f => f.Postagens).WithOne(p => p.Feed).IsRequired();
        }

    }
}

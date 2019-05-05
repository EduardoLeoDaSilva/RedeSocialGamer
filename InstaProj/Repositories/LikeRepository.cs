using InstaProj.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public class LikeRepository : ILikeRepository
    {

        private readonly ApplicationContext _context;
        private readonly IServiceProvider _provider;

        public LikeRepository(ApplicationContext context, IServiceProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public void AddLike(Like like)
        {
            if(like != null)
            {
            _context.Set<Like>().Add(like);
                _context.SaveChanges();
                
            }
            else
            {
                throw new ArgumentException("Argumento informado é nulo", nameof(like));
            }
        }

        public void RemoveLike(int postagemId, int usuarioId)
        {
            var contextTemp = _provider.CreateScope().ServiceProvider.GetService<ApplicationContext>();
            //var contextTemp = new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer("Data Source=DUD-PC;Initial Catalog=InstaGamer;User ID=sa;Password=02121441;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options);
              //  var likeDb = _context.Set<Like>().Where(l=> l.PostagemId == like.PostagemId && l.UsuarioAutorId == like.UsuarioAutorId).SingleOrDefault();
                try
                {


                    var changeTracker = contextTemp.Set<Like>().Remove(new Like(postagemId, usuarioId));
                    Console.WriteLine("Estado: " + changeTracker.State);


                contextTemp.SaveChanges();
                    Console.WriteLine("Estado: " + changeTracker.State);

                }
                catch (Exception e)
                {

                    throw;
                }
                  


                

            }

            
        }

    }


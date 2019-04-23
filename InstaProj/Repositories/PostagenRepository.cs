using InstaProj.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public class PostagenRepository : IPostagenRepository
    {

        private readonly ApplicationContext _context;

        public PostagenRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Postagem GetUltimaPostagem()
        {
            var postagem = _context.Postagens.Last();
            return postagem;
        }


        public Postagem GetPostagemById(int id)
        {
            var postagem = _context.Postagens.Where(p => p.PostagemId == id).SingleOrDefault();
            return postagem;
        }


        public Postagem AddPostagem(Postagem postagem)
        {

            _context.Set<Postagem>().Add(postagem);
            _context.SaveChanges();
            return postagem;
        }
    }
}

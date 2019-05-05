using InstaProj.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationContext _context;

        public ComentarioRepository(ApplicationContext context)
        {
            _context = context;
        }


        public Comentario AddComentario(Comentario comentario)
        {
            var comentarioReturn = comentario;
            if(comentario != null)
            {
                _context.Add(comentarioReturn);
                _context.SaveChanges();
                return comentarioReturn;
            }
            else
            {
                throw new ArgumentException("Argumento infomado inválido", nameof(comentario),
                    new NullReferenceException( "Valor nulo do do argumento"+ nameof(comentario)));
            }
        }

        public List<Comentario> GetComentarios()
        {
            return null;
        }

    }
}

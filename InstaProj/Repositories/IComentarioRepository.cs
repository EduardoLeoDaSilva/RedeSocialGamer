using System.Collections.Generic;
using InstaProj.Models.Entidades;

namespace InstaProj.Repositories
{
    public interface IComentarioRepository
    {
        Comentario AddComentario(Comentario comentario);
        List<Comentario> GetComentarios();
    }
}
using InstaProj.Models.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public interface IPostagenRepository
    {
        Postagem GetUltimaPostagem();
        Postagem GetPostagemById(int id);
        Postagem AddPostagem(Postagem postagem);
        List<Postagem> GetPostagens();
    }
}
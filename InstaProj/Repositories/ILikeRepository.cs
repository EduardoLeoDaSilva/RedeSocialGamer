using InstaProj.Models.Entidades;

namespace InstaProj.Repositories
{
    public interface ILikeRepository
    {
        void AddLike(Like like);
        void RemoveLike(int postagemId,int usuarioId);
    }
}
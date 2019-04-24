using System.Collections.Generic;
using InstaProj.Models.Entidades;

namespace InstaProj.Repositories
{
    public interface IAmigoRepository
    {
        List<Amigo> GetAmigosSemImagem(int idUsuario);
        List<Usuario> GetUsuarioNaoAmigos(string email);
        void AddAmigo(string email, int id);
    }
}
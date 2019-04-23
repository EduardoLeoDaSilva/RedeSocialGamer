using System.Collections.Generic;
using InstaProj.Models.Entidades;

namespace InstaProj.Repositories
{
    public interface IImagemRepository
    {
        byte[] GetImagensPostagem(int idImagem);
    }
}
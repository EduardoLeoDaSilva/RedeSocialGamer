using System.Collections.Generic;
using InstaProj.Models.Entidades;

namespace InstaProj.Repositories
{
    public interface INoticiasRepository
    {
        IEnumerable<Noticias> GetNoticias();
        Noticias GetUltimaNoticia();
    }
}
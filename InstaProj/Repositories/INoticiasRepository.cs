using System.Collections.Generic;
using System.Threading.Tasks;
using InstaProj.Models.Entidades;

namespace InstaProj.Repositories
{
    public interface INoticiasRepository
    {
        Task <IEnumerable<Noticias>> GetNoticias();
        Noticias GetUltimaNoticia();
    }
}
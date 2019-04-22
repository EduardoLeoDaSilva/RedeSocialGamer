using InstaProj.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public class NoticiasRepository : INoticiasRepository
    {

        private readonly ApplicationContext _context;

        public NoticiasRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Noticias>> GetNoticias()
        {
            var noticiasBd = _context.Set<Noticias>().ToList().TakeLast(10);
            var taskFactory = new TaskFactory();
            return await taskFactory.StartNew(()=> { 
            if(noticiasBd.Count()> 0)
            {
                return noticiasBd;
            }
            return null;
            })
;        }


        public Noticias GetUltimaNoticia()
        {
            var noticiaBd = _context.Noticias.LastOrDefault();
            if(noticiaBd != null)
            {
                return noticiaBd;
            }
            return null;
        }


    }
}

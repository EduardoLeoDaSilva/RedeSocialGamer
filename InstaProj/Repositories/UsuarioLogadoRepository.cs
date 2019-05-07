using InstaProj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    public class UsuarioLogadoRepository : IUsuarioLogadoRepository
    {

        private readonly ApplicationContext _context;

        public UsuarioLogadoRepository(ApplicationContext context)
        {
            _context = context;
        }


        public void AddLoggedUser(int userId)
        {
            _context.Database.OpenConnection();

            _context.Set<UsuarioLogado>().Add(new UsuarioLogado(userId));
           var tt=  _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.UsuarioLogado ON");

                _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.UsuarioLogado OFF");
            _context.Database.CloseConnection();
        }

       public List<int> GetLoggedUsersid()
        {
            var usersIds = _context.Set<UsuarioLogado>().Select(p => p.UsuarioLogadoId).ToList();
            return usersIds;

        }

        public void RemoveLoggedUser(int userId)
        {
                var user = _context.Set<UsuarioLogado>().Where(u => u.UsuarioLogadoId == userId).SingleOrDefault();
                _context.Set<UsuarioLogado>().Remove(user);
                _context.SaveChanges();
        }

    }
}

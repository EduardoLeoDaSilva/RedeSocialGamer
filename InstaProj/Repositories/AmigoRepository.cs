using InstaProj.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{


    public class AmigoRepository : IAmigoRepository
    {
        private readonly ApplicationContext _context;

        public AmigoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Amigo> GetAmigos(int idUsuario)
        {
            var amigos = _context.Set<Amigo>().Where(a => a.Usuario.UsuarioId == idUsuario).ToList();
            return amigos;
        }


        public List<Usuario> GetUsuarioNaoAmigos(string email)
        {
            var usuario = _context.Set<Usuario>().Include(u => u.Amigos).ThenInclude(a => a.UsuarioAmigo).Where(u => u.Email == email).SingleOrDefault();
            var usuarios = _context.Set<Usuario>().Include(u => u.Amigos).ThenInclude(a => a.UsuarioAmigo).Where(u => u.Email != email).ToList();
            var listaDeAmigos = usuario.Amigos.Select(p => p.UsuarioAmigo).ToList();
            var listaNaoAmigos = usuarios.Except(listaDeAmigos).ToList();
            return listaNaoAmigos;
        }


        public void AddAmigo(string email, int id)
        {
            var userQEnviouSolicitacao = _context.Set<Usuario>().Where(u => u.Email == email).SingleOrDefault();
            var usuarioAAdd = _context.Set<Usuario>().Where(u => u.UsuarioId == id).SingleOrDefault();
            if(usuarioAAdd != null && userQEnviouSolicitacao != null)
            {
                var amigo = new Amigo(usuarioAAdd, userQEnviouSolicitacao);
                
                _context.Add(amigo);
                _context.SaveChanges();
            }
        }


    }
}

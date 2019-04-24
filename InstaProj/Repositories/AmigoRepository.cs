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

        public List<Amigo> GetAmigosSemImagem(int idUsuario)
        {
            var amigos = _context.Set<Amigo>().Include(a => a.Usuario).Include(a => a.UsuarioAmigo).Where(a => a.Usuario.UsuarioId == idUsuario).ToList();
            var listaAmigosSemFoto = new List<Amigo>();
            if(amigos.Count > 0)
            {
                foreach (var item in amigos)
                {
                    listaAmigosSemFoto.Add(new Amigo(new Usuario(item.UsuarioAmigo.UsuarioId,item.UsuarioAmigo.Nome, item.UsuarioAmigo.Sexo, item.UsuarioAmigo.Email, null,null,item.UsuarioAmigo.Nascimento),
                        new Usuario(item.Usuario.UsuarioId, item.Usuario.Nome, item.Usuario.Sexo, item.Usuario.Email, null, null, item.Usuario.Nascimento)));
                }
                return listaAmigosSemFoto;
            }
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
                var amigo2 = new Amigo(userQEnviouSolicitacao, usuarioAAdd);

                _context.Set<Amigo>().AddRange(amigo, amigo2);
                _context.SaveChanges();
            }
        }


    }
}

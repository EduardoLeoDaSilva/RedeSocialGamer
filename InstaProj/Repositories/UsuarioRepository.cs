using InstaProj.Models.Entidades;
using InstaProj.Models.extencoes;
using InstaProj.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Repositories
{
    
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }
     

        public async Task CadastrarUsuario(UsuarioViewModel usuarioView)
        {
            var usuario = new Usuario(usuarioView.Nome, usuarioView.Sexo, usuarioView.Email, null, usuarioView.ObterFotoBytes(), usuarioView.Nascimento);
            var user = await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();

        }

        public  Usuario GetUsuarioPorEmail(string email)
        {
            var usuario = _context.Set<Usuario>().Where(user => user.Email == email).SingleOrDefault();
            return usuario;
        }

    }
}

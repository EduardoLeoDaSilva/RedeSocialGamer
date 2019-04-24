using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{
    public class Amigo
    {
        public Amigo()
        {

        }

        public Amigo( Usuario usuarioAmigo, Usuario usuario)
        {
            UsuarioAmigo = usuarioAmigo;
            Usuario = usuario;
        }

        public int AmigoId { get; private set; }
        public Usuario UsuarioAmigo { get; private set; }
        public Usuario Usuario { get; set; }

    }
}

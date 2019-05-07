using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Models
{
    public class UsuarioLogado
    {
        public int UsuarioLogadoId { get; set; }

        public UsuarioLogado(int usuarioLogadoId)
        {
            UsuarioLogadoId = usuarioLogadoId;
        }
    }
}

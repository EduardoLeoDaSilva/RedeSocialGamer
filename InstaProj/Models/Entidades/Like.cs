using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{
    [DataContract]
    public class Like
    {

        [DataMember]
        public int PostagemId { get; private set; }
        [DataMember]
        public int UsuarioAutorId { get; private set; }
        [DataMember]
        public Postagem Postagem { get; private set; }
        [DataMember]
        public Usuario UsuarioAutor { get; private set; }


        public Like()
        {

        }

        public Like(int postagemId, int usuarioAutorId)
        {
            PostagemId = postagemId;
            UsuarioAutorId = usuarioAutorId;
        }

        public Like(Postagem postagem, Usuario usuarioAutor)
        {
            Postagem = postagem;
            UsuarioAutor = usuarioAutor;
        }
    }
}

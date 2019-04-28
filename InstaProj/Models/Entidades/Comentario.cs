using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{
    [DataContract]
    public class Comentario
    {
        [DataMember]
        public int ComentarioId { get; private set; }
        [DataMember]
        public Postagem Postagem { get; private set; }
        [DataMember]
        public Usuario UsuarioAutor { get; private set; }

        public Comentario()
        {

        }

        public Comentario(Postagem postagem, Usuario usuarioAutor)
        {
            Postagem = postagem;
            UsuarioAutor = usuarioAutor;
        }

        public Comentario(int comentarioId, Postagem postagem, Usuario usuarioAutor)
        {
            ComentarioId = comentarioId;
            Postagem = postagem;
            UsuarioAutor = usuarioAutor;
        }
    }
}

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
        [DataMember]
        public string ComentarioTexto { get; private set; }
        [DataMember]
        public DateTime ComentarioData { get; set; }
        public Comentario()
        {

        }

        public Comentario(Postagem postagem, Usuario usuarioAutor)
        {
            Postagem = postagem;
            UsuarioAutor = usuarioAutor;
        }

        public Comentario(Postagem postagem, Usuario usuarioAutor, string comentarioTexto)
        {
            Postagem = postagem;
            UsuarioAutor = usuarioAutor;
            ComentarioTexto = comentarioTexto;
        }

        public Comentario(Postagem postagem, Usuario usuarioAutor, string comentarioTexto, DateTime comentarioData) : this(postagem, usuarioAutor, comentarioTexto)
        {
            ComentarioData = comentarioData;
        }
    }
}

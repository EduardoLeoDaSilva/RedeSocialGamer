using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{
    [DataContract]
    public class Postagem
    {

        public Postagem()
        {

        }

        public Postagem(int postagemId, Usuario usuario)
        {
            PostagemId = postagemId;
            Usuario = usuario;
        }

        [DataMember]
        public int PostagemId  { get; private set; }
        [DataMember]
        public Usuario Usuario { get; private set; }
        [DataMember]
        public Feed Feed { get; private set; }
        [DataMember]
        public byte[] imagem { get; private set; }

    }
}

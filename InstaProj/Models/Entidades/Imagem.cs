using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{
    [DataContract]
    public class Imagem
    {
        [DataMember]

        public int ImagemId { get;private set; }

        public Postagem Postagem { get;private set; }
        [DataMember]
        public byte[] foto { get; private set; }

        public Imagem()
        {

        }

        public Imagem(Postagem postagem, byte[] foto)
        {
            Postagem = postagem;
            this.foto = foto;
        }

        public Imagem(int imagemId, Postagem postagem, byte[] foto)
        {
            ImagemId = imagemId;
            Postagem = postagem;
            this.foto = foto;
        }

        public Imagem(byte[] foto)
        {
            this.foto = foto;
        }
    }
}

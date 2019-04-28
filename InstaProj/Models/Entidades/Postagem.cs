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

        public Postagem(Usuario usuario, List<Imagem> imagens, string texto)
        {
            Usuario = usuario;
            Imagens = imagens;
            Texto = texto;
        }

        [DataMember]
        public int PostagemId  { get; private set; }
        [DataMember]
        public Usuario Usuario { get; private set; }
        //[DataMember]
        //public Feed Feed { get; private set; }
        [DataMember]
        public List<Imagem> Imagens { get; private set; }
        [DataMember]
        public string Texto { get; private set; }
        [DataMember]
        public List<Comentario> Comentarios { get; private set; }


    }
}

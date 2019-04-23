using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.ViewModels
{
    [DataContract]
    public class PostagemViewModel
    {

        public PostagemViewModel()
        {

        }

        public PostagemViewModel(int postagemId, List<string> imagens, string texto)
        {
            PostagemId = postagemId;
            Imagens = imagens;
            Texto = texto;
        }

        [DataMember]
        public int PostagemId { get; private set; }
        //[DataMember]
        //public Feed Feed { get; private set; }
        [DataMember]
        public List<string> Imagens { get; private set; }
        [DataMember]
        public string Texto { get; private set; }


    }
}

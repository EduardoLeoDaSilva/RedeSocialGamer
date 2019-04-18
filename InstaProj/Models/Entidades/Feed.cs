using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{
    [DataContract]
    public class Feed
    {

        public Feed()
        {

        }

        public Feed(int feedId, List<Postagem> postagens)
        {
            FeedId = feedId;
            Postagens = postagens;
        }

        [DataMember]
        public int FeedId { get; private set; }
        [DataMember]
        public List<Postagem> Postagens { get; private set; }
    }
}

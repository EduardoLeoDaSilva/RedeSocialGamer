using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.Entidades
{


    [DataContract]
    public class Usuario
    {


        public Usuario()
        {
                
        }

        public Usuario( string nome, string sexo, string email, string senha, byte[] foto, DateTime nascimento)
        {
            Nome = nome;
            Sexo = sexo;
            Email = email;
            Senha = senha;
            this.foto = foto;
            Nascimento = nascimento;
        }

        [DataMember]
        public int UsuarioId { get; private set; }
        [DataMember]
        public string Nome { get; private set; }
        [DataMember]
        public string Sexo { get; private set; }
        [DataMember]
        public string Email { get; private set; }
        [DataMember]
        public string Senha { get; private set; }
        [DataMember]
        public byte[] foto { get; private set; }
        [DataMember]
        public DateTime Nascimento { get; private set; }
        [DataMember]
        public List<Postagem> Postagens { get; private set; }

    }
}

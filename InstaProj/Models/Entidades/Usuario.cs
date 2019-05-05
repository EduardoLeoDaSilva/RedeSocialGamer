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

        public Usuario(int usuarioId, string nome, string sexo, string email, string senha, byte[] foto, DateTime nascimento)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Sexo = sexo;
            Email = email;
            Senha = senha;
            this.foto = foto;
            Nascimento = nascimento;
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
        public int UsuarioId { get;  set; }
        [DataMember]
        public string Nome { get;  set; }
        [DataMember]
        public string Sexo { get;  set; }
        [DataMember]
        public string Email { get;  set; }
        [DataMember]
        public string Senha { get;  set; }
        [DataMember]
        public byte[] foto { get;  set; }
        [DataMember]
        public DateTime Nascimento { get;  set; }
        public virtual List<Postagem> Postagens { get; set; }
        [DataMember]
        public List<Amigo> Amigos { get; set; }
    }
}

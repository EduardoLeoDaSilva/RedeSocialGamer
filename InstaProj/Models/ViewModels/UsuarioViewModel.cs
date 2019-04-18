using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InstaProj.Models.ViewModels
{
    [DataContract]
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(string nome, string sexo, DateTime nascimento, string email, string password, string confirmPassword, IFormFile foto)
        {
            Nome = nome;
            Sexo = sexo;
            Nascimento = nascimento;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            Foto = foto;
        }

        [DataMember]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [DataMember]
        [Display(Name ="Sexo")]
        public string Sexo { get;  set; }
        [DataMember]
        [DataType(DataType.Text, ErrorMessage = "Data inválida")]
        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime Nascimento { get; set; }
        [DataMember]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email inválido")]
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get;  set; }
        [DataMember]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [DataMember]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "As senhas são diferentes")]
        [Required(ErrorMessage = "O campo Confirme sua senha é obrigatório")]
        [Display(Name = "Confirme sua senha")]
        public string ConfirmPassword { get;  set; }
        [DataMember]
        [Required(ErrorMessage = "O upload de uma foto é necessário")]
        [Display(Name = "Envie uma fota")]
        public IFormFile Foto { get; set; }

    }
}

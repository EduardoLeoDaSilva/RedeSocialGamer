using System.Collections.Generic;
using System.Threading.Tasks;
using InstaProj.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace InstaProj.Identity
{
    public interface IAutenticaoRepository
    {
        Task<List<IdentityResult>> AddNewUsuario(UsuarioViewModel usuarioViewModel);
        Task<SignInResult> RealizarLogin(string email, string senha);
        Task RealizarLogOff();
    }
}
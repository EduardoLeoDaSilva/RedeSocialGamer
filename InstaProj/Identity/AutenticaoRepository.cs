using InstaProj.Models.Identity;
using InstaProj.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Identity
{
    public class AutenticaoRepository : IAutenticaoRepository
    {
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly SignInManager<UsuarioIdentity> _signInManager;

        public AutenticaoRepository(UserManager<UsuarioIdentity> userManager, SignInManager<UsuarioIdentity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<List<IdentityResult>> AddNewUsuario(UsuarioViewModel usuarioViewModel)
        {
            var usuarioIdentity = new UsuarioIdentity();
            usuarioIdentity.Email = usuarioViewModel.Email;
            usuarioIdentity.UserName = usuarioViewModel.Email;
            List<IdentityResult> identityResults = new List<IdentityResult>();

            var result = await _userManager.CreateAsync(usuarioIdentity);
            var user = await _userManager.FindByEmailAsync(usuarioViewModel.Email);
            identityResults.Add(result);
            if (result.Succeeded)
            {
                var resultSenha = await _userManager.AddPasswordAsync(user, usuarioViewModel.Password);
                identityResults.Add(resultSenha);
            }
            return identityResults;
        }


        public async Task<SignInResult> RealizarLogin(string email, string senha)
        {
            var result = await _signInManager.PasswordSignInAsync(email, senha, false, true);
            return result;
        }

        public async Task RealizarLogOff()
        {
            await _signInManager.SignOutAsync();
        }
    }

}


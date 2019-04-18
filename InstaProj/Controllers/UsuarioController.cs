using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaProj.Identity;
using InstaProj.Models.Identity;
using InstaProj.Models.ViewModels;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstaProj.Controllers
{
    public class UsuarioController : Controller
    {

    
        private readonly IUsuarioRepository _usuarioRepository;
        // private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly IAutenticaoRepository _autenticaoRepository;



        public UsuarioController(IUsuarioRepository usuarioRepository, UserManager<UsuarioIdentity> userManager, IAutenticaoRepository autenticaoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _autenticaoRepository = autenticaoRepository;
        }

        [HttpPost]
        public async Task<string> Cadastrar([FromForm]UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _usuarioRepository.CadastrarUsuario(usuario);
                    var result =await _autenticaoRepository.AddNewUsuario(usuario);
                    return "Usuario cadastrado com sucesso!";

                }
                catch (Exception e)
                {

                    Console.Write(e.Message);
                    return "Erro: "+e.Message;


                }
            }
            return "Erro";

        }

   
        public async Task<string> EfetuarLogIn([FromBody]UsuarioViewModel user)
        {
            try
            {
               var result = await _autenticaoRepository.RealizarLogin(user.Email , user.Password);

                if (result.Succeeded)
                {
                    return "Success";
                }

                if (result.IsLockedOut)
                {
                    return "LockedOut";
                }

                if (result.IsNotAllowed)
                {
                    return "NotAllowed)";
                }

            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }
            return "CrendentialsWrong";

        }

        public async Task<IActionResult> EfetuarLogOff()
        {
            try
            {
               await _autenticaoRepository.RealizarLogOff();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return RedirectToAction("index", "Principal");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaProj.Controllers.ControllersTempoReal;
using InstaProj.Identity;
using InstaProj.Models;
using InstaProj.Models.Identity;
using InstaProj.Models.ViewModels;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace InstaProj.Controllers
{
    public class UsuarioController : Controller
    {

    
        private readonly IUsuarioRepository _usuarioRepository;
        // private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly IAutenticaoRepository _autenticaoRepository;
        private readonly IHubContext<PostagemHub> _hubPostagem;
        private readonly IUsuarioLogadoRepository _logadoRepository;


        public UsuarioController(IUsuarioRepository usuarioRepository, UserManager<UsuarioIdentity> userManager, IAutenticaoRepository autenticaoRepository,
            IHubContext<PostagemHub> hubPostagem, IUsuarioLogadoRepository logadoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _autenticaoRepository = autenticaoRepository;
            _hubPostagem = hubPostagem;
            _logadoRepository = logadoRepository;
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
                    var usuario = _usuarioRepository.GetUsuarioPorEmail(user.Email);
                    await _hubPostagem.Clients.All.SendAsync("RebecerUserOnlineOffline", usuario.UsuarioId, true);

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
                var usuario = _usuarioRepository.GetUsuarioPorEmail(HttpContext.User.Identity.Name);

                await _hubPostagem.Clients.All.SendAsync("RebecerUserOnlineOffline", usuario.UsuarioId, false);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaProj.Models.Entidades;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstaProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private  Usuario usuarioLogado;


        public HomeController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            
        }
        public IActionResult Home()
        {
            usuarioLogado = _usuarioRepository.GetUsuarioPorEmail(User.Identity.Name);
            return View(usuarioLogado);
        }

        public async Task<FileResult> CarregarImagem()
        {
            var fabricaTask = new TaskFactory();
            var imagem = await fabricaTask.StartNew(() =>
            {
                var imagemBytes = _usuarioRepository.GetUsuarioPorEmail(User.Identity.Name).foto;
                if (imagemBytes == null)
                {
                    return null;
                }
                return File(imagemBytes, "image/png");
            }
            );
            return imagem;
        }
     }
}
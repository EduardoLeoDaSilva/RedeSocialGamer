using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaProj.BancoDados;
using InstaProj.Models.Entidades;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using Microsoft.AspNetCore.SignalR;
using InstaProj.Controllers.ControllersTempoReal;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base;
using InstaProj.Models.ViewModels;
using InstaProj.Models.extencoes;
using System.Security.Claims;

namespace InstaProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INoticiasRepository _noticiasRepository;
        private readonly IPostagenRepository postagenRepository;
        private readonly IHubContext<PostagemHub> hubPostage;
        private Usuario usuarioLogado;
        
        public HomeController(IUsuarioRepository usuarioRepository, INoticiasRepository noticiasRepository, IPostagenRepository postagenRepository, IHubContext<PostagemHub> hubPostagem)
        {
            _usuarioRepository = usuarioRepository;
            _noticiasRepository = noticiasRepository;
            this.postagenRepository = postagenRepository;
            this.hubPostage = hubPostagem;




        }

        

        public IActionResult Home()
        {
           
            usuarioLogado = _usuarioRepository.GetUsuarioPorEmail(User.Identity.Name);
            return View(usuarioLogado);
        }

        public async Task<FileResult> CarregarImagemPerfil()
        {
            FileContentResult imagem = await ObterImagem(_usuarioRepository.GetUsuarioPorEmail(User.Identity.Name).foto);
            return imagem;
        }

        
        public async Task<FileResult> CarregarImagemPostagem([FromRoute]int id)
        {
            var imagemBytes = postagenRepository.GetPostagemById(id).Imagem;
            FileContentResult imagem = await ObterImagem(imagemBytes);
            return imagem;
        }

        private async Task<FileContentResult> ObterImagem(byte[] imagemBytes)
        {
            var fabricaTask = new TaskFactory();
            var imagem = await fabricaTask.StartNew(() =>
            {
                if (imagemBytes == null)
                {
                    return null;
                }
                return File(imagemBytes, "image/png");
            }
            );
            return imagem;
        }

        [HttpGet]
        public async Task<IEnumerable<Noticias>> GetNoticias()
        {
            var noticias = await _noticiasRepository.GetNoticias();
            return noticias;
        }

        [HttpGet]
        public Noticias GetUltimaNoticia()
        {
            var ultimaNoticia = _noticiasRepository.GetUltimaNoticia();
            return ultimaNoticia;
        }

        [HttpPost]
        public async void EnviarPostagem()
        {
            try
            {
                var texto = HttpContext.Request.Form["texto"];
                var foto = HttpContext.Request.Form.Files["foto"];
                var usuarioLogadoEmail = HttpContext.User.Identity.Name;
                PostagemViewModel postagemViewModel = InsereERecuperaPostagemNova(texto, foto, usuarioLogadoEmail);

                //await hubPostage.Clients.User(userLoggedId).SendAsync("ReceberPostagem", postagemViewModel);
                await hubPostage.Clients.All.SendAsync("ReceberPostagem", postagemViewModel);
            }
            catch (Exception e)
            {
                await hubPostage.Clients.All.SendAsync("ReceberErro", e.Message);

                
            }

            await hubPostage.Clients.All.SendAsync("ReceberErro", "Ocorreu um erro desconhecido, por favor tente mais tarde!");

        }

        private  PostagemViewModel InsereERecuperaPostagemNova(Microsoft.Extensions.Primitives.StringValues texto, Microsoft.AspNetCore.Http.IFormFile foto, string usuarioLogadoEmail)
        {
            var usuario = _usuarioRepository.GetUsuarioPorEmail(usuarioLogadoEmail);
            var imagemBytes = Extencoes.LerStreamFoto(foto);
            var postagem = new Postagem(usuario, imagemBytes, texto);
            postagem = postagenRepository.AddPostagem(postagem);
            var userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var postagemViewModel = postagem.toViewModel();
            return postagemViewModel;
        }

    }
}



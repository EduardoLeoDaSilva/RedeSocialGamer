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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using InstaProj.Models.Identity;
using Microsoft.Extensions.Primitives;

namespace InstaProj.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly INoticiasRepository _noticiasRepository;
        private readonly IPostagenRepository _postagenRepository;
        private readonly IHubContext<PostagemHub> _hubPostagem;
        private readonly IImagemRepository _imagemRepository;
        private readonly IAmigoRepository _amigoRepository;
        private readonly IUsuarioLogadoRepository _usuarioLogado;
      //  private Usuario usuarioLogado;
        private readonly UserManager<UsuarioIdentity> _userManager;

        public HomeController(IUsuarioRepository usuarioRepository, INoticiasRepository noticiasRepository, IPostagenRepository postagenRepository
            , IHubContext<PostagemHub> hubPostagem, IImagemRepository imagemRepository, IAmigoRepository amigoRepository,
            UserManager<UsuarioIdentity> userManager, IUsuarioLogadoRepository usuarioLogado)
        {
            _usuarioRepository = usuarioRepository;
            _noticiasRepository = noticiasRepository;
            _postagenRepository = postagenRepository;
            _hubPostagem = hubPostagem;
            _imagemRepository = imagemRepository;
            _amigoRepository = amigoRepository;
            _userManager = userManager;
            _usuarioLogado = usuarioLogado;
        }

        public IActionResult Home()
        {

            Usuario usuarioLogado = _usuarioRepository.GetUsuarioPorEmail(User.Identity.Name);

            return View(usuarioLogado);
        }


        public async Task<FileResult> CarregarImagemPerfil()
        {
            FileContentResult imagem = await ObterImagem(_usuarioRepository.GetUsuarioPorEmail(User.Identity.Name).foto);
            return imagem;
        }

        public async Task<FileResult> CarregarImagemPostagem([FromRoute]int id)
        {
            var imagemBytes = _imagemRepository.GetImagensPostagem(id);
            FileContentResult imagem = await ObterImagem(imagemBytes);
            return imagem;
        }

        public async Task<FileResult> CarregarImagemUsuario([FromRoute]string id)
        {
            var imagemBytes = _imagemRepository.GetImagemUsuario(id);
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
                var foto = HttpContext.Request.Form.Files;
                var usuarioLogadoEmail = HttpContext.User.Identity.Name;

                var postagemViewModel = InsereERecuperaPostagemNova(texto, foto, usuarioLogadoEmail);

                //await hubPostage.Clients.User(userLoggedId).SendAsync("ReceberPostagem", postagemViewModel);
                await _hubPostagem.Clients.All.SendAsync("ReceberPostagem", postagemViewModel);
            }
            catch (Exception e)
            {
                await _hubPostagem.Clients.All.SendAsync("ReceberErro", e.Message);


            }
            await _hubPostagem.Clients.All.SendAsync("ReceberErro", "Ocorreu um erro desconhecido, por favor tente mais tarde!");

        }
        private PostagemViewModel InsereERecuperaPostagemNova(StringValues texto, IFormFileCollection foto, string usuarioLogadoEmail)
        {
            var usuario = _usuarioRepository.GetUsuarioPorEmail(usuarioLogadoEmail);
            var imagemBytes = foto.toListaBytes();
            List<Imagem> listaImagens = ListaBytesToListaImagens(imagemBytes);

            var postagem = new Postagem(usuario, listaImagens, texto);
            postagem = _postagenRepository.AddPostagem(postagem);
            var userLoggedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var postagemViewModel = postagem.toViewModel();
            return postagemViewModel;
        }

        private static List<Imagem> ListaBytesToListaImagens(List<byte[]> imagemBytes)
        {
            var listaImagens = new List<Imagem>();
            foreach (var imagemByte in imagemBytes)
            {

                listaImagens.Add(new Imagem(imagemByte));
            }

            return listaImagens;
        }

        [HttpGet]
        public List<Usuario> ListarNaoAmigos()
        {

            var lista = _amigoRepository.GetUsuarioNaoAmigos(HttpContext.User.Identity.Name);
            if (lista != null)
            {

                return lista;
            }
            return null;


        }

        [HttpPost]
        public async Task<string> AddAmigo([FromBody]int id)
        {
            var email = HttpContext.User.Identity.Name;
            if (email != null)
            {
                try
                {
                    _amigoRepository.AddAmigo(email, id);
                    await EnviarNovaListaAmigosTempoReal(id);
                    return "Adiconado com sucesso";
                }
                catch (Exception e)
                {

                    return e.Message;
                }
            }
            return "Ocorreu um erro, tente novamente mais tarde";


        }

        private async Task EnviarNovaListaAmigosTempoReal(int id)
        {
            var usuarioBd = _usuarioRepository.GetUsuarioPorEmail(HttpContext.User.Identity.Name);
            var usuarioAmigo = _usuarioRepository.GetUsuarioPorId(id);
            var listaAmigosUsuarioConectado = _amigoRepository.GetAmigosSemImagem(usuarioBd.UsuarioId);
           var listaAmigosUsuarioAmigo = _amigoRepository.GetAmigosSemImagem(usuarioAmigo.UsuarioId);
            var idUsuarioConectado = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            var listaUsuarioConecatadoNaoAmigos = _amigoRepository.GetUsuarioNaoAmigos(usuarioBd.Email);
            var listaUsuarioAmigoNaoAmigos = _amigoRepository.GetUsuarioNaoAmigos(usuarioAmigo.Email);


            var usuarioAmigoIdentity = await _userManager.FindByEmailAsync(usuarioAmigo.Email);
            var usuarioAmigoId = usuarioAmigoIdentity.Id;

            await _hubPostagem.Clients.User(idUsuarioConectado).SendAsync("AoAdicionarAmigo", listaAmigosUsuarioConectado);
            await _hubPostagem.Clients.User(usuarioAmigoId).SendAsync("AoAdicionarAmigo", listaAmigosUsuarioAmigo);
            await _hubPostagem.Clients.User(idUsuarioConectado).SendAsync("AtualizarListaNaoAmigos", listaUsuarioConecatadoNaoAmigos);
            await _hubPostagem.Clients.User(usuarioAmigoId).SendAsync("AtualizarListaNaoAmigos",  listaUsuarioAmigoNaoAmigos);





        }

        [HttpPost]
        public List<Amigo> ObterListaDeAmigos()
        {

            var usuarioBd = _usuarioRepository.GetUsuarioPorEmail(HttpContext.User.Identity.Name);

            var listaAmigos = _amigoRepository.GetAmigosSemImagem(usuarioBd.UsuarioId);
            if(listaAmigos.Count > 0)
            {
                return listaAmigos;
            }
            return null;

        }

        public List<PostagemViewModel> GetPostagens()
        {
            var postagens = _postagenRepository.GetPostagens();
            var postagensViewModel = new List<PostagemViewModel>();
            if(postagens != null)
            {
                foreach (var postagem in postagens)
                {
                    postagensViewModel.Add(postagem.toViewModel());

                }
                return postagensViewModel;
            }
            throw new Exception("Sem postagens no Banco");
        }

        public List<int> GetUsuarioLogados()
        {
            var lista = _usuarioLogado.GetLoggedUsersid();
            return lista;
        }

        public async void RemoveUsuarioLogado()
        {
            var user = _usuarioRepository.GetUsuarioPorEmail(HttpContext.User.Identity.Name);
            _usuarioLogado.RemoveLoggedUser(user.UsuarioId);


            await _hubPostagem.Clients.All.SendAsync("ReceberUserOffline", user.UsuarioId);
        }

        [HttpPost]
        public async void VericarERemoveSeUserEstaLogado()
        {
            Usuario usuarioLogado = _usuarioRepository.GetUsuarioPorEmail(User.Identity.Name);

            var usuarioLogados = _usuarioLogado.GetLoggedUsersid();
            var userLogadoNesteContexto = usuarioLogados.Where(u => u == usuarioLogado.UsuarioId).SingleOrDefault();
            if (userLogadoNesteContexto != 0)
            {
                _usuarioLogado.RemoveLoggedUser(usuarioLogado.UsuarioId);
                _usuarioLogado.AddLoggedUser(usuarioLogado.UsuarioId);

            }
            else
            {
                _usuarioLogado.AddLoggedUser(usuarioLogado.UsuarioId);

            }
            await  _hubPostagem.Clients.All.SendAsync("RebecerUserOnlineOffline", usuarioLogado.UsuarioId, true);

        }

    }


}
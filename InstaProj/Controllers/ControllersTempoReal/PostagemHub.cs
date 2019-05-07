using InstaProj.BancoDados;
using InstaProj.Models.Entidades;
using InstaProj.Models.Identity;
using InstaProj.Models.ViewModels;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Delegates;

namespace InstaProj.Controllers.ControllersTempoReal
{
    public class PostagemHub : Hub
    {
        private readonly IConfiguration configuration;
        private readonly SqlTableDependency<Postagem> notificaBanco;
        private readonly IPostagenRepository _postagenRepository;
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IComentarioRepository _comentarioRepository;
        private ApplicationContext context;
        private  IHubCallerClients Clientes;
        private HubCallerContext Contexto;
        public PostagemHub(IPostagenRepository postagenRepository, NotificacaoDbPostagens notificaBanco, IConfiguration configuration, ApplicationContext context,
            UserManager<UsuarioIdentity> userManager, IUsuarioRepository usuarioRepository, ILikeRepository likeRepository, IComentarioRepository comentarioRepository)
        {
            this._postagenRepository = postagenRepository;
            this.notificaBanco = notificaBanco;
            //notificaBanco.OnChanged += NotificaBanco_OnChanged;
            this.configuration = configuration;
            //notificaBanco.Start();
            this.context = context;
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _likeRepository = likeRepository;
            _comentarioRepository = comentarioRepository;
        }

        //private async void NotificaBanco_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Postagem> e)
        //{
        //    var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(configuration.GetConnectionString("Default"));
        //    using (var dbContext = new ApplicationContext(dbContextOptions.Options))
        //    {
        //        var postagem = dbContext.Set<Postagem>().First();
        //        await Clientes.User(Contexto.UserIdentifier).SendAsync("ReceberPostagem", postagem);

        //    }
        //}

        public async Task sendPostagem(string dad)
        {
            var userIdentifier = Context.UserIdentifier;
            await Clients.User(Context.UserIdentifier).SendAsync("ReceberPostagem", userIdentifier); 
        }


        public async Task EnviarMensagem(string email, string mensagem)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var usuarioConectado = _usuarioRepository.GetUsuarioPorEmail(Context.User.Identity.Name);
            await Clients.Users(user.Id).SendAsync("ReceberMensagem", usuarioConectado, mensagem);
        }
        
        public async Task VerificarDigitacao(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var usuarioConectado = _usuarioRepository.GetUsuarioPorEmail(Context.User.Identity.Name);
            await Clients.Users(user.Id).SendAsync("ReceberDigitacao", usuarioConectado, usuarioConectado.Nome + " Está digitando...");
        }

        public async Task SendALike(int idPostagem)
        {
            var userQDeuOLike = _usuarioRepository.GetUsuarioPorEmail(Context.User.Identity.Name);
            var postagem = _postagenRepository.GetPostagemById(idPostagem);
            var like= new Like(postagem, userQDeuOLike);
            try
            {
                _likeRepository.AddLike(like);
                await Clients.All.SendAsync("ReceberLikeReload", new { like.PostagemId,like.UsuarioAutorId});

            }
            catch (Exception E)
            {
                if(E.HResult == -2146233088)
                {
                        _likeRepository.RemoveLike(postagem.PostagemId, userQDeuOLike.UsuarioId); 
                   await Clients.All.SendAsync("ReceberLikeReload", new { like.PostagemId, like.UsuarioAutorId, isLiked= true });
                }
                
            }



       
        }

        public async Task SendComentario(int idPostagem, string texto)
        {
            var userQEnvouComent = _usuarioRepository.GetUsuarioPorEmail(Context.User.Identity.Name);
            var postagem = _postagenRepository.GetPostagemById(idPostagem);
            var comentario = new Comentario(postagem, userQEnvouComent, texto, DateTime.Now);
            try
            {
               var comentarioSalvo = _comentarioRepository.AddComentario(comentario);
                
                ///Setando alguns valores nulos que nao serao utilizados na view para tornar o transporte mais rapido
                comentarioSalvo.UsuarioAutor.foto = null;
                comentario.Postagem.SetImagens(null);
                //------------

                await Clients.All.SendAsync("ReceberComentario",comentarioSalvo);
            }
            catch (Exception E)
            {
                await Clients.All.SendAsync("ReceberComentario", E.Message);


            }

        }
    }
}

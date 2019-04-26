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
        private readonly IPostagenRepository postagenRepository;
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private ApplicationContext context;
        private  IHubCallerClients Clientes;
        private HubCallerContext Contexto;
        public PostagemHub(IPostagenRepository postagenRepository, NotificacaoDbPostagens notificaBanco, IConfiguration configuration, ApplicationContext context,
            UserManager<UsuarioIdentity> userManager, IUsuarioRepository usuarioRepository)
        {
            this.postagenRepository = postagenRepository;
            this.notificaBanco = notificaBanco;
            //notificaBanco.OnChanged += NotificaBanco_OnChanged;
            this.configuration = configuration;
            //notificaBanco.Start();
            this.context = context;
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
        }

        public override Task OnConnectedAsync()
        {
            //Clientes = this.Clients;
            //Contexto = this.Context;
           
            return base.OnConnectedAsync();
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
        
        public async Task VerificarDigitacao(string email , string msg)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var usuarioConectado = _usuarioRepository.GetUsuarioPorEmail(Context.User.Identity.Name);
            await Clients.Users(user.Id).SendAsync("ReceberDigitacao", usuarioConectado, msg);
        }
    }
}

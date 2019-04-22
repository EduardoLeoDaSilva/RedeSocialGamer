using InstaProj.Controllers.ControllersTempoReal;
using InstaProj.Models.Entidades;
using InstaProj.Models.Identity;
using InstaProj.Repositories;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.Delegates;
using TableDependency.SqlClient.Base.Enums;


namespace InstaProj.BancoDados
{
    public class NotificacaoDbPostagens : SqlTableDependency<Postagem>
    {

        //        private SqlTableDependency<Postagem> _tableDependency;
        //        private IHubContext<PostagemHub> hubContext;
        //        public bool IsChanged { get; set; }
        //        private readonly IHttpContextAccessor httpContext;
        //        private readonly IServiceCollection services;
        //        //public PostagemBancoOuvidor(IHubContext<PostagemHub> hubContext ,IHttpContextAccessor httpContext, IServiceCollection services)
        //        //{

        //        //    this.hubContext = hubContext;
        //        //    this.services = services;
        //        //    this.httpContext = httpContext;
        //        //}

        //        public PostagemBancoOuvidor()
        //        {

        //        }


        //        public void Configure()
        //        {

        //            var mapper = new ModelToTableMapper<Postagem>();
        //            mapper.AddMapping(p => p.PostagemId, "PostagemId");

        //            _tableDependency =
        //                new SqlTableDependency<Postagem>
        //                ("Data Source=DUD-PC;Initial Catalog=InstaGamer;User ID=sa;Password=02121441;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "Postagens", null, null, null, null, DmlTriggerType.Insert);
        //            _tableDependency.OnChanged += _tableDependency_OnChanged;
        //            _tableDependency.Start();

        //        }

        //        public void Dispose()
        //        {
        //            _tableDependency.Dispose();

        //        }

        //        private async void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Postagem> e)
        //        {
        //            if (e.ChangeType != ChangeType.None)
        //            {


        //                var postagem = db.Set<Postagem>().First();
        //                await hubContext.Clients.User(httpContext.HttpContext.User.Identity.Name).SendAsync("ReceberPostagem", postagem);
        //            }







        //        }
        public NotificacaoDbPostagens() : base("Data Source=DUD-PC;Initial Catalog=InstaGamer;User ID=sa;Password=02121441;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", "Postagens", null, null, null, null, DmlTriggerType.Insert)
        {

        }

       

    }





}
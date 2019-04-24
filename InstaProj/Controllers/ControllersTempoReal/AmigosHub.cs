using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaProj.Controllers.ControllersTempoReal
{
    public class AmigosHub : Hub
    {

        public AmigosHub()
        {

        }


        //public async Task AtualizarListaAmigos(int idUsuario, int idAmigo)
        //{
        //    await Clients.User(Context.UserIdentifier).SendAsync("AoAdicionarAmigo", userIdentifier);
        //} 

    }
}

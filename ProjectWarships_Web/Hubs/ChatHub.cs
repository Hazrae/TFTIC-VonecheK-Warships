using Microsoft.AspNetCore.SignalR;
using ProjectWarships_Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWarships_Web.Hubs
{
    public class ChatHub : Hub
    {
        ISessionManager _session;
        public ChatHub(ISessionManager session) { _session = session; }
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", _session.Login, message);
        }
    }
}

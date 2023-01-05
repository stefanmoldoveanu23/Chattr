using ClassLibrary.Models.DTOs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(LogDTO message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}

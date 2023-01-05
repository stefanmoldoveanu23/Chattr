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
        public async void JoinGroup(String group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(LogDTO message, String group)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", message);
        }
    }
}

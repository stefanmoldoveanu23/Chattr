using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Helpers.Utils;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Services.ServerService;
using ClassLibrary.Services.UserService;
using Discord_Copycat.Models.Enums;
using Microsoft.AspNetCore.Authorization;
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
            /*string? token = Context.GetHttpContext().Request.Query["access_token"];
            if (token == null || token == "")
            {
                return;
            }

            Guid userId = _jwtUtils.ValidateJwtToken(token);

            Console.WriteLine("Join Group " + userId);*/
            Console.WriteLine(Context.GetHttpContext().Request.Headers["Authorization"]);
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async void SendMessage(LogRequestDTO message, String group)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", new LogResponseDTO(message));
        }
    }
}

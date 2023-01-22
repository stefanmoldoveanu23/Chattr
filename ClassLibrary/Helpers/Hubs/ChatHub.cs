﻿using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.DTOs.LogDTO;
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
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async void SendMessage(LogRequestDTO message, String group)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", new LogResponseDTO(message));
        }
    }
}

using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models.DTOs
{
    internal class LogDTO
    {
        public Guid UserId { get; set; }
        public String Message { get; set; } = "";

        public LogDTO(ChatLog ChatLog)
        {
            UserId = ChatLog.SenderId;
            Message = ChatLog.Message;
        }

        public LogDTO(FriendLog FriendLog)
        {
            UserId = FriendLog.SenderId;
            Message = FriendLog.Message;
        }
    }
}

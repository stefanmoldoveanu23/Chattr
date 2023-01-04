using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.ChatService
{
    internal interface IChatService
    {
        Task<List<Chat>> GetChatsAsync();

        Task<Chat?> GetChatByIdAsync(Guid id);

        Task CreateChatAsync(Chat chat);

        void UpdateChat(Chat chat);

        void DeleteChat(Chat chat);

        Task<List<User>> GetUsersAsync(Guid id);

        Task<List<ChatLog>> GetLogsAsync(Guid id);
    }
}

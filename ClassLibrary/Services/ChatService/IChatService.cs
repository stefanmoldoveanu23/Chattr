using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.ChatService
{
    public interface IChatService
    {
        Task<List<ChatResponseDTO>> GetChatsAsync();

        Task<ChatResponseDTO?> GetChatByIdAsync(Guid id);

        Task<ChatResponseDTO> CreateChatAsync(ChatRequestDTO chat);

        void UpdateChat(Chat chat);

        void DeleteChat(Chat chat);

        Task<List<UserResponseDTO>?> GetUsersAsync(Guid id);

        Task<List<LogResponseDTO>?> GetLogsAsync(Guid id);
    }
}

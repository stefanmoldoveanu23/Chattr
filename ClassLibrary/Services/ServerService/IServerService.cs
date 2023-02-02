using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.ServerService
{
    public interface IServerService
    {
        Task<List<ServerResponseDTO>> GetServersAsync();

        Task<ServerResponseDTO?> GetServerByIdAsync(Guid id);

        Task<ServerResponseDTO> CreateServerAsync(ServerRequestDTO server);

        void UpdateServer(Server server);

        void DeleteServer(Server server);

        Task<List<UserResponseDTO>?> GetUsersAsync(Guid id);

        Task<List<ChatResponseDTO>?> GetChatsAsync(Guid id);

        Task<List<ChatResponseDTO>?> GetChatsForRoleAsync(Guid id, Roles role);
    }
}

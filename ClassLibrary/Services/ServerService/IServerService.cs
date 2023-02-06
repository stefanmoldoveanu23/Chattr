using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Enums;

namespace ClassLibrary.Services.ServerService
{
    public interface IServerService
    {
        Task<List<ServerResponseDTO>> GetServersAsync();

        Task<ServerResponseDTO?> GetServerByIdAsync(Guid id);

        Task<string?> GetServerToken(Guid id);

        Task<ServerResponseDTO> CreateServerAsync(ServerRequestDTO server);

        void UpdateServer(Server server);

        void DeleteServer(Server server);

        Task<List<UserResponseDTO>?> GetUsersAsync(Guid id);

        Task<Roles?> GetUserRole(Guid id, Guid userId);

        Task<List<ChatResponseDTO>?> GetChatsAsync(Guid id);

        Task<List<ChatResponseDTO>?> GetChatsForUserAsync(Guid id, Guid userId);
    }
}

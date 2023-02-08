using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using Discord_Copycat.Models;
using ClassLibrary.Models.DTOs.LogDTO;
using Discord_Copycat.Models.Enums;

namespace ClassLibrary.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetUsersAsync();

        Task<UserResponseDTO?> GetUserByIdAsync(Guid id);

        Task<UserResponseDTO> CreateUserAsync(UserRequestDTO userDTO);

        void UpdateUser(User user);

        void DeleteUser(User user);

        Task DeleteUserByIdAsync(Guid id);

        Task<List<UserResponseDTO>> GetFriendsAsync(Guid id);

        Task<LogResponseDTO?> SendMessageAsync(Guid id, Guid friendId, string message);

        Task<LogResponseDTO?> DeleteMessageAsync(Guid id, Guid friendId, Guid logId);

        Task<Guid?> GetFriendshipAsync(Guid id, Guid friendId);

        Task<UserResponseDTO?> AddFriendAsync(Guid id, Guid friendId);

        Task<UserResponseDTO?> RemoveFriendAsync(Guid id, Guid friendId);

        Task<List<ServerResponseDTO>> GetServersAsync(Guid id);

        Task<UserResponseDTO?> JoinServerAsync(Guid id, Guid serverId, Roles role);

        Task<UserResponseDTO?> LeaveServerAsync(Guid id, Guid serverId);

        Task<List<LogResponseDTO>?> GetLogsWithFriendAsync(Guid id, Guid friendId);

        Task<User> GetWithSettingsAsync(Guid id);

        UserResponseDTO? Authenticate(UserRequestDTO req);
    }
}

using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        Task<List<UserResponseDTO>> GetFriendsAsync(Guid id);

        Task<LogResponseDTO?> SendMessage(Guid id, Guid friendId, string message);

        Task<Guid?> GetFriendshipAsync(Guid id, Guid friendId);

        Task<UserResponseDTO?> AddFriend(Guid id, Guid friendId);

        Task<UserResponseDTO?> RemoveFriend(Guid id, Guid friendId);

        Task<List<ServerResponseDTO>> GetServersAsync(Guid id);

        Task<UserResponseDTO?> JoinServerAsync(Guid id, Guid serverId, Roles role);

        Task<List<LogResponseDTO>?> GetLogsWithFriendAsync(Guid id, Guid friendId);

        Task<User> GetWithSettingsAsync(Guid id);

        UserResponseDTO? Authenticate(UserRequestDTO req);
    }
}

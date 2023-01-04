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

namespace ClassLibrary.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetUsersAsync();

        Task<UserResponseDTO?> GetUserByIdAsync(Guid id);

        Task CreateUserAsync(UserRequestDTO userDTO);

        void UpdateUser(User user);

        void DeleteUser(User user);

        Task<List<UserResponseDTO>> GetFriendsAsync(Guid id);

        Task<List<ServerResponseDTO>> GetServersAsync(Guid id);

        Task<List<LogDTO>?> GetLogsWithFriendAsync(Guid id, Guid friendId);

        Task<User> GetWithSettingsAsync(Guid id);
    }
}

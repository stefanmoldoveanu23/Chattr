using ClassLibrary.Models.DTOs;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.UserService
{
    internal interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<User?> GetUserByIdAsync(Guid id);

        Task CreateUserAsync(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        Task<List<User>> GetFriendsAsync(Guid id);

        Task<List<Server>> GetServersAsync(Guid id);

        Task<List<FriendLog>?> GetLogsWithFriendAsync(Guid id, Guid friendId);

        Task<User> GetWithSettingsAsync(Guid id);
    }
}

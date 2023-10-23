using ClassLibrary.Repositories.GenericRep;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.UserRep
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetWithFriendAsync(Guid id, Guid friendId);

        Task<User> GetWithFriendsAsync(Guid id);

        Task<User> GetWithServersAsync(Guid id);

        Task<User> GetWithSettingsAsync(Guid id);

        Task<User?> GetWithLogsAsync(Guid id, Guid friendId);

        User? FindByData(String username, String password);
    }
}

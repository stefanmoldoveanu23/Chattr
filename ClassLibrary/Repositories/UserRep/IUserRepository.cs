using ClassLibrary.Repositories.GenericRep;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.UserRep
{
    internal interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetWithFriendsAsync(Guid id);

        Task<User> GetWithServersAsync(Guid id);

        Task<User> GetWithSettingsAsync(Guid id);

        Task<User?> GetWithLogsAsync(Guid id, Guid friendId);
    }
}

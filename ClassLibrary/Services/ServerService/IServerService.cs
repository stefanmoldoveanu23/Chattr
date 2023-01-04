using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.ServerService
{
    internal interface IServerService
    {
        Task<List<Server>> GetServersAsync();

        Task<Server?> GetServerByIdAsync(Guid id);

        Task CreateServerAsync(Server server);

        void UpdateServer(Server server);

        void DeleteServer(Server server);

        Task<List<User>> GetUsersAsync(Guid id);

        Task<List<Chat>> GetChatsAsync(Guid id);
    }
}

using ClassLibrary.Repositories.GenericRep;
using Discord_Copycat.Data;
using Discord_Copycat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.ServerRep
{
    internal class ServerRepository : GenericRepository<Server>, IServerRepository
    {
        public ServerRepository(DiscordContext discordContext) : base(discordContext) { }

        public async Task<Server?> GetWithChats(Guid id)
        {
            Server? chats = await _table.Where(s => s.Id == id)
                .Include(s => s.Chats)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            return chats;
        }

        public async Task<Server?> GetWithUsers(Guid id)
        {
            Server? users = await _table.Where(s => s.Id == id)
                .Include(s => s.Users)
                .ThenInclude(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            return users;
        }
    }
}

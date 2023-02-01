using ClassLibrary.Repositories.GenericRep;
using Discord_Copycat.Data;
using Discord_Copycat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.ChatRep
{
    internal class ChatRepository: GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(DiscordContext discordContext) : base(discordContext) { }

        public async Task<Chat?> GetWithLogs(Guid id)
        {
            Chat? logs = await _table.Where(c => c.Id == id)
                .Include(c => c.Logs)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            return logs;
        }

        public async Task<Chat?> GetWithUsers(Guid id)
        {
            Chat? users = await _table.Where(c => c.Id == id)
                .Include(c => c.Server)
                .ThenInclude(s => s.Users)
                .ThenInclude(u => u.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (users == null)
            {
                return null;
            }

            users.Server.Users = users.Server.Users.Where(u => u.Role >= users.Role).ToList();

            return users;
        }
    }
}

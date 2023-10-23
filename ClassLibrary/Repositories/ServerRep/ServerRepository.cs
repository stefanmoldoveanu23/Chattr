using ClassLibrary.Repositories.GenericRep;
using Chattr.Data;
using ClassLibrary.Models;
using ClassLibrary.Models.Enums;
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
        public ServerRepository(ChattrContext chattrContext) : base(chattrContext) { }

        public async Task<Server?> GetWithChats(Guid id)
        {
            Server? chats = await _table.Where(s => s.Id == id)
                .Include(s => s.Chats)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            return chats;
        }

        public async Task<Roles?> GetUserRole(Guid id, Guid userId)
        {
            Roles? role = await _table.Where(server => server.Id == id).Join(_chattrContext.Members,
                    server => server.Id,
                    member => member.ServerId,
                    (server, member) => new
                    {
                        member.UserId,
                        member.Role
                    }).Where(member => member.UserId == userId)
                    .Select(member => member.Role)
                    .FirstOrDefaultAsync();

            return role;
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

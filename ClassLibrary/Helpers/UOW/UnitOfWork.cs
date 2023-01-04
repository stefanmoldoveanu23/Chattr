using ClassLibrary.Repositories.ChatRep;
using ClassLibrary.Repositories.ServerRep;
using ClassLibrary.Repositories.UserRep;
using Discord_Copycat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DiscordContext _discordContext;
        public IUserRepository _userRepository { get; }
        public IServerRepository _serverRepository { get; }
        public IChatRepository _chatRepository { get; }

        public UnitOfWork(DiscordContext discordContext)
        {
            _discordContext = discordContext;
            _userRepository = new UserRepository(_discordContext);
            _serverRepository = new ServerRepository(_discordContext);
            _chatRepository = new ChatRepository(_discordContext);
        }

        public async Task SaveAsync()
        {
            await _discordContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _discordContext.Dispose();
            }
        }

    }
}

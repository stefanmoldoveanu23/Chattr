using ClassLibrary.Repositories.ChatRep;
using ClassLibrary.Repositories.ServerRep;
using ClassLibrary.Repositories.UserRep;
using Discord_Copycat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.UnitOfWork
{
    internal class UnitOfWork
    {
        protected DiscordContext _discordContext;
        public IUserRepository _userRepository;
        public IServerRepository _serverRepository;
        public IChatRepository _chatRepository;

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

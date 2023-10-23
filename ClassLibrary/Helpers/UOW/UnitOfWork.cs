using Chattr.Data;
using ClassLibrary.Repositories.ChatRep;
using ClassLibrary.Repositories.ServerRep;
using ClassLibrary.Repositories.UserRep;
using Chattr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ChattrContext _chattrContext;
        public IUserRepository _userRepository { get; }
        public IServerRepository _serverRepository { get; }
        public IChatRepository _chatRepository { get; }

        public UnitOfWork(ChattrContext discordContext)
        {
            _chattrContext = discordContext;
            _userRepository = new UserRepository(_chattrContext);
            _serverRepository = new ServerRepository(_chattrContext);
            _chatRepository = new ChatRepository(_chattrContext);
        }

        public void Save()
        {
            _chattrContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _chattrContext.SaveChangesAsync();
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
                _chattrContext.Dispose();
            }
        }

    }
}

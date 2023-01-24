using ClassLibrary.Repositories.ChatRep;
using ClassLibrary.Repositories.ServerRep;
using ClassLibrary.Repositories.UserRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository _userRepository { get; }
        public IServerRepository _serverRepository { get; }
        public IChatRepository _chatRepository { get; }

        void Save();
        Task SaveAsync();
    }
}

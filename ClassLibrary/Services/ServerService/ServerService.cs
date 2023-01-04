using ClassLibrary.Helpers.UOW;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.ServerService
{
    internal class ServerService : IServerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateServerAsync(Server server)
        {
            await _unitOfWork._serverRepository.CreateAsync(server);
        }

        public void DeleteServer(Server server)
        {
            _unitOfWork._serverRepository.Delete(server);
        }

        public async Task<List<Chat>> GetChatsAsync(Guid id)
        {
            Server server = await _unitOfWork._serverRepository.GetWithChats(id);

            List<Chat> chats = new();
            foreach (Chat chat in server.Chats)
            {
                chats.Add(chat);
            }

            return chats;
        }

        public async Task<Server?> GetServerByIdAsync(Guid id)
        {
            return await _unitOfWork._serverRepository.FindByIdAsync(id);
        }

        public async Task<List<Server>> GetServersAsync()
        {
            return await _unitOfWork._serverRepository.GetAllAsync();
        }

        public async Task<List<User>> GetUsersAsync(Guid id)
        {
            Server server = await _unitOfWork._serverRepository.GetWithUsers(id);

            List<User> users = new();

            foreach (MemberOfServer memberOfServer in server.Users)
            {
                users.Add(memberOfServer.User);
            }

            return users;
        }

        public void UpdateServer(Server server)
        {
            _unitOfWork._serverRepository.Update(server);
        }
    }
}

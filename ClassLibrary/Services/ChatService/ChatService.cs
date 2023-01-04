using ClassLibrary.Helpers.UOW;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.ChatService
{
    internal class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateChatAsync(Chat chat)
        {
            await _unitOfWork._chatRepository.CreateAsync(chat);
        }

        public void DeleteChat(Chat chat)
        {
            _unitOfWork._chatRepository.Delete(chat);
        }

        public async Task<Chat?> GetChatByIdAsync(Guid id)
        {
            return await _unitOfWork._chatRepository.FindByIdAsync(id);
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            return await _unitOfWork._chatRepository.GetAllAsync();
        }

        public async Task<List<ChatLog>> GetLogsAsync(Guid id)
        {
            Chat chat = await _unitOfWork._chatRepository.GetWithLogs(id);

            List<ChatLog> logs = new();

            foreach (ChatLog log in chat.Logs)
            {
                logs.Add(log);
            }

            return logs;
        }

        public async Task<List<User>> GetUsersAsync(Guid id)
        {
            Chat chat = await _unitOfWork._chatRepository.GetWithUsers(id);

            List<User> users = new();

            foreach (MemberOfServer memberOfServer in chat.Server.Users)
            {
                users.Add(memberOfServer.User);
            }

            return users;
        }

        public void UpdateChat(Chat chat)
        {
            _unitOfWork._chatRepository.Update(chat);
        }
    }
}

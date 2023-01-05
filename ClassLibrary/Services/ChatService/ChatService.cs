using AutoMapper;
using ClassLibrary.Helpers.UOW;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.UserDTO;
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
        private readonly IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateChatAsync(ChatRequestDTO chat)
        {
            await _unitOfWork._chatRepository.CreateAsync(_mapper.Map<Chat>(chat));
            await _unitOfWork.SaveAsync();
        }

        public void DeleteChat(Chat chat)
        {
            _unitOfWork._chatRepository.Delete(chat);
            _unitOfWork.Save();
        }

        public async Task<ChatResponseDTO?> GetChatByIdAsync(Guid id)
        {
            Chat? chat = await _unitOfWork._chatRepository.FindByIdAsync(id);
            if (chat == null)
            {
                return null;
            }

            return _mapper.Map<ChatResponseDTO>(chat);
        }

        public async Task<List<ChatResponseDTO>> GetChatsAsync()
        {
            List<ChatResponseDTO> chats = new();
            foreach (Chat chat in await _unitOfWork._chatRepository.GetAllAsync())
            {
                chats.Add(_mapper.Map<ChatResponseDTO>(chat));
            }

            return chats;
        }

        public async Task<List<LogResponseDTO>> GetLogsAsync(Guid id)
        {
            Chat chat = await _unitOfWork._chatRepository.GetWithLogs(id);

            List<LogResponseDTO> logs = new();

            foreach (ChatLog log in chat.Logs)
            {
                logs.Add(_mapper.Map<LogResponseDTO>(log));
            }

            return logs;
        }

        public async Task<List<UserResponseDTO>> GetUsersAsync(Guid id)
        {
            Chat chat = await _unitOfWork._chatRepository.GetWithUsers(id);

            List<UserResponseDTO> users = new();

            foreach (MemberOfServer memberOfServer in chat.Server.Users)
            {
                users.Add(_mapper.Map<UserResponseDTO>(memberOfServer.User));
            }

            return users;
        }

        public void UpdateChat(Chat chat)
        {
            _unitOfWork._chatRepository.Update(chat);
            _unitOfWork.Save();
        }
    }
}

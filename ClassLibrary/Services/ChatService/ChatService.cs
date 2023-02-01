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

        public async Task<ChatResponseDTO> CreateChatAsync(ChatRequestDTO chat)
        {
            Chat newChat = _mapper.Map<Chat>(chat);
            await _unitOfWork._chatRepository.CreateAsync(newChat);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ChatResponseDTO>(newChat);
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

        public async Task<List<LogResponseDTO>?> GetLogsAsync(Guid id)
        {
            Chat? chat = await _unitOfWork._chatRepository.GetWithLogs(id);
            if (chat == null)
            {
                return null;
            }

            List<LogResponseDTO> logs = new();

            foreach (ChatLog log in chat.Logs)
            {
                logs.Add(_mapper.Map<LogResponseDTO>(log));
            }

            return logs;
        }

        public async Task<List<UserResponseDTO>?> GetUsersAsync(Guid id)
        {
            Chat? chat = await _unitOfWork._chatRepository.GetWithUsers(id);
            if (chat == null)
            {
                return null;
            }

            List<UserResponseDTO> users = new();

            foreach (MemberOfServer memberOfServer in chat.Server.Users)
            {
                users.Add(_mapper.Map<UserResponseDTO>(memberOfServer.User));
            }

            return users;
        }

        public async Task<LogResponseDTO?> SendMessage(Guid id, Guid userId, string message)
        {
            Chat? chat = await _unitOfWork._chatRepository.GetWithLogs(id);
            if (chat == null)
            {
                return null;
            }

            ChatLog log = new ChatLog { ChatId = id, SenderId = userId, Message = message };
            chat.Logs.Add(log);
            _unitOfWork._chatRepository.Update(chat);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<LogResponseDTO>(log);
        }

        public void UpdateChat(Chat chat)
        {
            _unitOfWork._chatRepository.Update(chat);
            _unitOfWork.Save();
        }
    }
}

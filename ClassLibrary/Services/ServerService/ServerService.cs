using AutoMapper;
using ClassLibrary.Helpers.UOW;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Enums;
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
        private readonly IMapper _mapper;

        public ServerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServerResponseDTO> CreateServerAsync(ServerRequestDTO server)
        {
            Server newServer = _mapper.Map<Server>(server);

            await _unitOfWork._serverRepository.CreateAsync(newServer);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ServerResponseDTO>(newServer);
        }

        public void DeleteServer(Server server)
        {
            _unitOfWork._serverRepository.Delete(server);
            _unitOfWork.Save();
        }

        public async Task<List<ChatResponseDTO>?> GetChatsAsync(Guid id)
        {
            Server server = await _unitOfWork._serverRepository.GetWithChats(id);
            if (server == null)
            {
                return null;
            }

            List<ChatResponseDTO> chats = new();
            foreach (Chat chat in server.Chats)
            {
                chats.Add(_mapper.Map<ChatResponseDTO>(chat));
            }

            return chats;
        }

        public async Task<List<ChatResponseDTO>?> GetChatsForRoleAsync(Guid id, Roles role)
        {
            List<ChatResponseDTO>? chats = await GetChatsAsync(id);
            if (chats == null)
            {
                return null;
            }

            return chats.FindAll(chat => chat.Role <= role);
        }

        public async Task<ServerResponseDTO?> GetServerByIdAsync(Guid id)
        {
            Server? server = await _unitOfWork._serverRepository.FindByIdAsync(id);
            if (server == null)
            {
                return null;
            }

            return _mapper.Map<ServerResponseDTO>(server);
        }

        public async Task<List<ServerResponseDTO>> GetServersAsync()
        {
            List<ServerResponseDTO> servers = new();
            foreach(Server server in await _unitOfWork._serverRepository.GetAllAsync())
            {
                servers.Add(_mapper.Map<ServerResponseDTO>(server));
            }

            return servers;
        }

        public async Task<List<UserResponseDTO>?> GetUsersAsync(Guid id)
        {
            Server? server = await _unitOfWork._serverRepository.GetWithUsers(id);
            if (server == null)
            {
                return null;
            }

            List<UserResponseDTO> users = new();

            foreach (MemberOfServer memberOfServer in server.Users)
            {
                users.Add(new UserResponseDTO(memberOfServer.User));
            }

            return users;
        }

        public void UpdateServer(Server server)
        {
            _unitOfWork._serverRepository.Update(server);
            _unitOfWork.Save();
        }
    }
}

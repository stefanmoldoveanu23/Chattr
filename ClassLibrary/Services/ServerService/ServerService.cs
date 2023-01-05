﻿using AutoMapper;
using ClassLibrary.Helpers.UOW;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
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
        private readonly IMapper _mapper;

        public ServerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateServerAsync(ServerRequestDTO server)
        {
            await _unitOfWork._serverRepository.CreateAsync(_mapper.Map<Server>(server));
            await _unitOfWork.SaveAsync();
        }

        public void DeleteServer(Server server)
        {
            _unitOfWork._serverRepository.Delete(server);
            _unitOfWork.Save();
        }

        public async Task<List<ChatResponseDTO>> GetChatsAsync(Guid id)
        {
            Server server = await _unitOfWork._serverRepository.GetWithChats(id);

            List<ChatResponseDTO> chats = new();
            foreach (Chat chat in server.Chats)
            {
                chats.Add(_mapper.Map<ChatResponseDTO>(chat));
            }

            return chats;
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

        public async Task<List<UserResponseDTO>> GetUsersAsync(Guid id)
        {
            Server server = await _unitOfWork._serverRepository.GetWithUsers(id);

            List<UserResponseDTO> users = new();

            foreach (MemberOfServer memberOfServer in server.Users)
            {
                users.Add(_mapper.Map<UserResponseDTO>(memberOfServer.User));
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

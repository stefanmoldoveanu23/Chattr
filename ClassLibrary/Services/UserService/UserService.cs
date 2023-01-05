﻿using AutoMapper;
using ClassLibrary.Helpers.UOW;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using Discord_Copycat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services.UserService
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserRequestDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);

            await _unitOfWork._userRepository.CreateAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public void DeleteUser(User user)
        {
            _unitOfWork._userRepository.Delete(user);
            _unitOfWork.Save();
        }

        public async Task<List<UserResponseDTO>> GetFriendsAsync(Guid id)
        {
            User user = await _unitOfWork._userRepository.GetWithFriendsAsync(id);
            List<UserResponseDTO> friends = new();

            foreach (Friendship friend in user.FirstFriend)
            {
                friends.Add(new UserResponseDTO(friend.User2, ""));
            }

            foreach (Friendship friend in user.SecondFriend)
            {
                friends.Add(new UserResponseDTO(friend.User1, ""));
            }

            return friends;
        }

        public async Task<List<LogResponseDTO>?> GetLogsWithFriendAsync(Guid id, Guid friendId)
        {
            User? user = await _unitOfWork._userRepository.GetWithLogsAsync(id, friendId);

            if (user == null)
            {
                return null;
            }

            List<LogResponseDTO> logs = new();

            if (user.FirstFriend != null)
            {
                foreach(FriendLog log in user.FirstFriend.First().Logs)
                {
                    logs.Add(_mapper.Map<LogResponseDTO>(log));
                }
            }
            else
            {
                foreach (FriendLog log in user.SecondFriend.First().Logs)
                {
                    logs.Add(_mapper.Map<LogResponseDTO>(log));
                }
            }

            return logs;
        }

        public async Task<List<ServerResponseDTO>> GetServersAsync(Guid id)
        {
            User user = await _unitOfWork._userRepository.GetWithServersAsync(id);

            List<ServerResponseDTO> servers = new();

            foreach (MemberOfServer memberOfServer in user.Servers)
            {
                servers.Add(_mapper.Map<ServerResponseDTO>(memberOfServer.Server));
            }

            return servers;
        }


        public async Task<UserResponseDTO?> GetUserByIdAsync(Guid id)
        {
            User? user = await _unitOfWork._userRepository.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserResponseDTO(user, "");
        }

        public async Task<List<UserResponseDTO>> GetUsersAsync()
        {
            List<UserResponseDTO> users = new();

            foreach (User user in await _unitOfWork._userRepository.GetAllAsync())
            {
                users.Add(new UserResponseDTO(user, ""));
            }

            return users;
        }

        public async Task<User> GetWithSettingsAsync(Guid id)
        {
            return await _unitOfWork._userRepository.GetWithSettingsAsync(id);
        }

        public void UpdateUser(User user)
        {
            _unitOfWork._userRepository.Update(user);
            _unitOfWork.Save();
        }
    }
}

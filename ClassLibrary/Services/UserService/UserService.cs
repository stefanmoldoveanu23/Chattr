using AutoMapper;
using ClassLibrary.Helpers.UOW;
using ClassLibrary.Helpers.Utils;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Models;
using ClassLibrary.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ClassLibrary.Services.UserService
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IJwtUtils jwtUtils, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> CreateUserAsync(UserRequestDTO userDTO)
        {
            userDTO.Password = BCryptNet.HashPassword(userDTO.Password);
            User user = _mapper.Map<User>(userDTO);

            await _unitOfWork._userRepository.CreateAsync(user);
            await _unitOfWork.SaveAsync();

            return new UserResponseDTO(user);
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
                friends.Add(new UserResponseDTO(friend.User2));
            }

            foreach (Friendship friend in user.SecondFriend)
            {
                friends.Add(new UserResponseDTO(friend.User1));
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

            if (user.FirstFriend.Count != 0)
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

        public UserResponseDTO? Authenticate(UserRequestDTO req)
        {
            User? user = _unitOfWork._userRepository.FindByData(req.Username, req.Password);

            if (user == null)
            {
                return null;
            }

            string jwtToken = _jwtUtils.GenerateJwtToken(user);
            return new UserResponseDTO(user, jwtToken);
        }

        public async Task<UserResponseDTO?> JoinServerAsync(Guid id, Guid serverId, Roles role)
        {
            User user = await _unitOfWork._userRepository.GetWithServersAsync(id);
            Server? server = await _unitOfWork._serverRepository.FindByIdAsync(serverId);
            if (server == null)
            {
                return null;
            }

            if (user.Servers.FirstOrDefault(server => server.ServerId == serverId) == null)
            {
                user.Servers.Add(new MemberOfServer { UserId = id, ServerId = serverId, Role = role });
                _unitOfWork._userRepository.Update(user);
                await _unitOfWork.SaveAsync();
            }

            return new UserResponseDTO(user);
        }

        public async Task<UserResponseDTO?> LeaveServerAsync(Guid id, Guid serverId)
        {
            User user = await _unitOfWork._userRepository.GetWithServersAsync(id);
            if (user.Servers.FirstOrDefault() is not MemberOfServer server)
            {
                return null;
            }

            user.Servers.Remove(server);
            _unitOfWork._userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return new UserResponseDTO(user);
        }

        public async Task<UserResponseDTO?> AddFriendAsync(Guid id, Guid friendId)
        {
            User user = await _unitOfWork._userRepository.GetWithFriendsAsync(id);
            User? friend = await _unitOfWork._userRepository.FindByIdAsync(friendId);

            if (friend == null)
            {
                return null;
            }

            user.FirstFriend.Add(new Friendship { User1Id = id, User2Id = friendId });
            _unitOfWork._userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return new UserResponseDTO(user);
        }

        public async Task<UserResponseDTO?> RemoveFriendAsync(Guid id, Guid friendId)
        {
            User user = await _unitOfWork._userRepository.GetWithFriendsAsync(id);
            User? friend = await _unitOfWork._userRepository.FindByIdAsync(friendId);

            if (friend == null)
            {
                return null;
            }

            Friendship? friendship = user.FirstFriend.FirstOrDefault(friendship => friendship.User2Id == friendId);
            if (friendship != null)
            {
                user.FirstFriend.Remove(friendship);
            } else
            {
                friendship = user.SecondFriend.FirstOrDefault(friendship => friendship.User1Id == friendId);
                if (friendship != null)
                {
                    user.SecondFriend.Remove(friendship);
                }

            }

            _unitOfWork._userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return new UserResponseDTO(user);
        }

        public async Task<Guid?> GetFriendshipAsync(Guid id, Guid friendId)
        {
            User? user = await _unitOfWork._userRepository.GetWithFriendAsync(id, friendId);
            if (user == null)
            {
                return null;
            }

            if (user.FirstFriend.Count == 1)
            {
                return user.FirstFriend.First().Id;
            } else
            {
                return user.SecondFriend.First().Id;
            }

        }

        public async Task<LogResponseDTO?> SendMessageAsync(Guid id, Guid friendId, string message)
        {
            User? user = await _unitOfWork._userRepository.GetWithLogsAsync(id, friendId);
            if (user == null)
            {
                return null;
            }

            FriendLog log;

            if (user.FirstFriend.Count == 1)
            {
                log = new FriendLog { FriendshipId = user.FirstFriend.First().Id, SenderId = id, Message = message };
                user.FirstFriend.First().Logs.Add(log);
            } else
            {
                log = new FriendLog { FriendshipId = user.SecondFriend.First().Id, SenderId = id, Message = message };
                user.SecondFriend.First().Logs.Add(log);
            }

            _unitOfWork._userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<LogResponseDTO>(log);
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            if (await _unitOfWork._userRepository.GetWithFriendsAsync(id) is User user)
            {
                user.FirstFriend.Clear();
                user.SecondFriend.Clear();
                await _unitOfWork.SaveAsync();

                DeleteUser(user);
            }
        }

        public async Task<LogResponseDTO?> DeleteMessageAsync(Guid id, Guid friendId, Guid logId)
        {
            if (await _unitOfWork._userRepository.GetWithLogsAsync(id, friendId) is not User user)
            {
                return null;
            }

            if (user.FirstFriend.Count() > 0)
            {
                if (user.FirstFriend.First().Logs.FirstOrDefault(log => log.Id == logId) is not FriendLog log)
                {
                    return null;
                }

                user.FirstFriend.First().Logs.Remove(log);
            } else if (user.SecondFriend.Count() > 0)
            {
                if (user.SecondFriend.First().Logs.FirstOrDefault(log => log.Id == logId) is not FriendLog log)
                {
                    return null;
                }

                user.SecondFriend.First().Logs.Remove(log);
            } else
            {
                return null;
            }

            _unitOfWork._userRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return new LogResponseDTO { Id = logId };
        }
    }
}

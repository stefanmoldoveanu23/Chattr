using ClassLibrary.Helpers.UnitOfWork;
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
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(User user)
        {
            await _unitOfWork._userRepository.CreateAsync(user);
            await _unitOfWork._userRepository.SaveAsync();
        }

        public void DeleteUser(User user)
        {
            _unitOfWork._userRepository.Delete(user);
        }

        public async Task<List<User>> GetFriendsAsync(Guid id)
        {
            User user = await _unitOfWork._userRepository.GetWithFriendsAsync(id);
            List<User> friends = new List<User>();

            foreach (Friendship friend in user.FirstFriend)
            {
                friends.Add(friend.User2);
            }

            foreach (Friendship friend in user.SecondFriend)
            {
                friends.Add(friend.User1);
            }

            return friends;
        }

        public async Task<List<FriendLog>?> GetLogsWithFriendAsync(Guid id, Guid friendId)
        {
            User? user = await _unitOfWork._userRepository.GetWithLogsAsync(id, friendId);

            if (user == null)
            {
                return null;
            }

            List<FriendLog> logs;

            if (user.FirstFriend != null)
            {
                logs = user.FirstFriend.First().Logs.ToList();
            } else
            {
                logs = user.SecondFriend.First().Logs.ToList();
            }

            return logs;
        }

        public async Task<List<Server>> GetServersAsync(Guid id)
        {
            User user = await _unitOfWork._userRepository.GetWithServersAsync(id);

            List<Server> servers = new List<Server>();

            foreach (MemberOfServer memberOfServer in user.Servers)
            {
                servers.Add(memberOfServer.Server);
            }

            return servers;
        }


        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _unitOfWork._userRepository.FindByIdAsync(id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _unitOfWork._userRepository.GetAllAsync();
        }

        public async Task<User> GetWithSettingsAsync(Guid id)
        {
            return await _unitOfWork._userRepository.GetWithSettingsAsync(id);
        }

        public void UpdateUser(User user)
        {
            _unitOfWork._userRepository.Update(user);
        }
    }
}

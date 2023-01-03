using Discord_Copycat.Data;
using Discord_Copycat.Models;
using Discord_Copycat.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace Discord_Copycat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DiscordContext _discordContext;

        public UserController(DiscordContext discordContext)
        {
            _discordContext = discordContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allUsers = await _discordContext.Users.ToListAsync();
            return Ok(allUsers);
        }

        [HttpGet("get-friends/{id}")]
        public async Task<IActionResult> GetFriends([FromRoute]Guid id)
        {
            var friends1 = await _discordContext.Friendships
                .Where(f => f.User1Id == id)
                .Select(f => f.User2)
                .ToListAsync();

            var friends2 = await _discordContext.Friendships
                .Where(f => f.User2Id == id)
                .Select(f => f.User1)
                .ToListAsync();

            return Ok(friends1.Concat(friends2));
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody]UserDTO User)
        {
            User newUser = new();
            newUser.Username = User.Username;
            newUser.Password = User.Password;
            newUser.Email = User.Email;

            await _discordContext.AddAsync(newUser);
            await _discordContext.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("add-friend/{Id}")]
        public async Task<IActionResult> AddFriend([FromRoute]Guid Id, [FromBody]UserDTO Friend)
        {
            var User = await _discordContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (User == null)
            {
                return Ok(null);
            }

            User newFriend = new();
            newFriend.Username = Friend.Username;
            newFriend.Password = Friend.Password;
            newFriend.Email = Friend.Email;

            Friendship friendship = new();
            friendship.User1 = User;
            friendship.User2 = newFriend;

            User.FirstFriend.Add(friendship);

            _discordContext.Update(User).State = EntityState.Modified;
            await _discordContext.SaveChangesAsync();

            return Ok(newFriend);
        }
    }
}

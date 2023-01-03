using Discord_Copycat.Data;
using Discord_Copycat.Models;
using Discord_Copycat.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserDTO User)
        {
            User newUser = new();
            newUser.Username = User.Username;
            newUser.Password = User.Password;
            newUser.Email= User.Email;

            await _discordContext.AddAsync(newUser);
            await _discordContext.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("add-friend")]
        public async Task<IActionResult> AddFriend([FromForm]Guid Id, [FromForm]UserDTO Friend)
        {
            var User = await _discordContext.Users.FirstOrDefaultAsync(x => x.Id == Id);

            User newFriend = new();
            newFriend.Username = Friend.Username;
            newFriend.Password = Friend.Password;
            newFriend.Email = Friend.Email;

            Friendship friendship = new();
            friendship.User1 = User;
            friendship.User2 = newFriend;

            User.FirstFriend.Add(friendship);
            newFriend.SecondFriend.Add(friendship);

            return Ok(newFriend);
        }
    }
}

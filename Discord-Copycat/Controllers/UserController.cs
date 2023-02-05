using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Helpers.Hubs;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Repositories.UserRep;
using ClassLibrary.Services.UserService;
using Discord_Copycat.Data;
using Discord_Copycat.Models;
using Discord_Copycat.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Discord_Copycat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-self")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public UserResponseDTO? GetSelf()
        {
            return HttpContext.Items["User"] as UserResponseDTO;
        }

        public async Task<IActionResult> GetAll()
        {
            var allUsers = await _userService.GetUsersAsync();
            return Ok(allUsers);
        }

        [HttpGet("get-friends")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetFriends()
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest("Error getting friends: no user logged in.");
            }

            List<UserResponseDTO> friends = (await _userService.GetFriendsAsync(User.Id));
            foreach (UserResponseDTO friend in friends)
            {
                friend.Email = "";
            }

            return Ok(friends);
        }

        [HttpGet("get-friendship/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetFriendship([FromRoute]Guid FriendId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error getting friendship id with ${FriendId}: no user logged in.");
            }

            Guid? FriendshipId = await _userService.GetFriendshipAsync(User.Id, FriendId);

            if (FriendshipId == null)
            {
                return NotFound($"Error getting friendship id with ${FriendId}: user ${User.Username} is not friends with ${FriendId}.");
            } else
            {
                return Ok(FriendshipId);
            }
        }

        [HttpGet("get-logs/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetLogsWithFriend([FromRoute]Guid FriendId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error getting chat logs with ${FriendId}: no user logged in.");
            }

            List<LogResponseDTO>? Logs = await _userService.GetLogsWithFriendAsync(User.Id, FriendId);
            if (Logs == null)
            {
                return NotFound($"Error getting chat logs with ${FriendId}: no friendship found between ${User.Username} and user with id {FriendId}.");
            }

            return Ok(Logs);
        }

        [HttpPost("add-friend/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> AddFriend([FromRoute]Guid FriendId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error adding friend with id {FriendId}: no user logged in.");
            }

            if (FriendId == User.Id)
            {
                return BadRequest($"Error adding friend with id {FriendId}: you cannot befriend yourself.");
            }

            if (await _userService.AddFriend(User.Id, FriendId) == null)
            {
                return NotFound($"Error adding friend with id {FriendId}: user does not exist.");
            }

            return Ok();
        }

        [HttpPut("send-message/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> SendMessage([FromRoute]Guid FriendId, [FromBody]LogRequestDTO Message)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error sending message to friend with id {FriendId}: no user logged in.");
            }

            LogResponseDTO? Log = await _userService.SendMessage(User.Id, FriendId, Message.Message);

            if (Log == null)
            {
                return NotFound($"Error sending message to friend: no friendship found between {User.Id} and {FriendId}.");
            }

            return Ok(Log);
        }

        [HttpPost("remove-friend/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> RemoveFriend([FromRoute]Guid FriendId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error removing friend with id {FriendId}: no user logged in.");
            }


            if (await _userService.RemoveFriend(User.Id, FriendId) == null)
            {
                return NotFound($"Error removing friend: no friendship found between {User.Id} and {FriendId}");
            }

            return Ok();
        }

        [HttpGet("get-by-id/{Id}")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid Id)
        {
            UserResponseDTO? User = await _userService.GetUserByIdAsync(Id);
            if (User == null)
            {
                return NotFound($"Error getting user with id {Id}: no such user exists.");
            }

            return Ok(User);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRequestDTO User)
        {
            await _userService.CreateUserAsync(User);
            return Ok();
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody]UserRequestDTO User)
        {
            UserResponseDTO? response = _userService.Authenticate(User);
            if (response == null)
            {
                return BadRequest($"Error logging in as \"{User.Username}\": no user found with these credentials.");
            }

            return Ok(response);
        }

        [HttpGet("get-servers")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetServers()
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest("Error getting servers: no user logged in.");
            }

            List<ServerResponseDTO> Servers = await _userService.GetServersAsync(User.Id);
            return Ok(Servers);
        }

        [HttpPost("join-server/{ServerId}/{Role}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> JoinServer([FromRoute]Guid ServerId, [FromRoute]Roles Role)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error joining server {ServerId}: no user logged in.");
            }

            if (await _userService.JoinServerAsync(User.Id, ServerId, Role) == null)
            {
                return NotFound($"Error joining server {ServerId}: no such server exists.");
            }
            return Ok();
        }
    }
}

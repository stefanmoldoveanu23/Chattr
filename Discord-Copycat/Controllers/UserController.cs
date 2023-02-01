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
        public UserResponseDTO GetSelf()
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
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            List<UserResponseDTO> friends = (await _userService.GetFriendsAsync(UserId));
            foreach (UserResponseDTO friend in friends)
            {
                friend.Email = "";
            }

            return Ok(friends);
        }

        [HttpGet("get-friendship/{friendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetFriendship([FromRoute]Guid friendId)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            Guid? FriendshipId = await _userService.GetFriendshipAsync(UserId, friendId);

            if (FriendshipId == null)
            {
                return NotFound();
            } else
            {
                return Ok(FriendshipId);
            }
        }

        [HttpGet("get-logs/{friendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetLogsWithFriend([FromRoute]Guid friendId)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            List<LogResponseDTO>? Logs = await _userService.GetLogsWithFriendAsync(UserId, friendId);
            if (Logs == null)
            {
                return NotFound();
            }

            return Ok(Logs);
        }

        [HttpPost("add-friend/{friendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> AddFriend([FromRoute]Guid friendId)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            if (friendId == UserId)
            {
                return BadRequest("You cannot befriend yourself.");
            }

            if (await _userService.AddFriend(UserId, friendId) == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("send-message/{friendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> SendMessage([FromRoute]Guid friendId, [FromBody]LogRequestDTO Message)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            LogResponseDTO? Log = await _userService.SendMessage(UserId, friendId, Message.Message);

            if (Log == null)
            {
                return NotFound();
            }
            Console.Write(Log.Message);

            return Ok(Log);
        }

        [HttpPost("remove-friend/{friendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> RemoveFriend([FromRoute]Guid friendId)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;

            if (await _userService.RemoveFriend(UserId, friendId) == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid id)
        {
            UserResponseDTO? User = await _userService.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound();
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
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("get-servers")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetServers()
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            List<ServerResponseDTO> Servers = await _userService.GetServersAsync(UserId);

            return Ok(Servers);
        }

        [HttpPost("join-server/{ServerId}/{role}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> JoinServer([FromRoute]Guid ServerId, [FromRoute]Roles role)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;

            if (await _userService.JoinServerAsync(UserId, ServerId, role) == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.UserService;
using ClassLibrary.Models.Enums;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Chattr.Controllers
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

        [HttpGet("self")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public IActionResult GetSelf()
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest("Error getting logged user: no user logged in.");
            }

            return Ok(User);
        }

        [HttpGet]
        [Authorization(Roles.Admin, Roles.Mod)]
        public async Task<IActionResult> GetAll()
        {
            var allUsers = await _userService.GetUsersAsync();
            return Ok(allUsers);
        }

        [HttpDelete]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> DeleteAccount()
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest("Error deleting account: no user logged in.");
            }

            await _userService.DeleteUserByIdAsync(User.Id);
            return Ok(); 
        }

        [HttpGet("friends")]
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

        [HttpGet("friendship/{FriendId}")]
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

        [HttpGet("logs/{FriendId}")]
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

        [HttpPost("friend/{FriendId}")]
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

            if (await _userService.AddFriendAsync(User.Id, FriendId) == null)
            {
                return NotFound($"Error adding friend with id {FriendId}: user does not exist.");
            }

            return Ok();
        }

        [HttpPost("log/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> SendMessage([FromRoute]Guid FriendId, [FromBody]LogRequestDTO Message)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error sending message to friend with id {FriendId}: no user logged in.");
            }

            LogResponseDTO? Log = await _userService.SendMessageAsync(User.Id, FriendId, Message.Message);

            if (Log == null)
            {
                return NotFound($"Error sending message to friend: no friendship found between {User.Id} and {FriendId}.");
            }

            return Ok(Log);
        }

        [HttpDelete("log/{FriendId}/{LogId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> DeleteMessage([FromRoute]Guid FriendId, [FromRoute]Guid LogId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error deleting message to friend with id {FriendId}: no user logged in.");
            }

            if (await _userService.DeleteMessageAsync(User.Id, FriendId, LogId) is null)
            {
                return NotFound($"Error deleting message: message with id {LogId} sent to friend with id {FriendId} by user with id {User.Id} was not found.");
            }

            return Ok();
        }

        [HttpDelete("friend/{FriendId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> RemoveFriend([FromRoute]Guid FriendId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error removing friend with id {FriendId}: no user logged in.");
            }


            if (await _userService.RemoveFriendAsync(User.Id, FriendId) == null)
            {
                return NotFound($"Error removing friend: no friendship found between {User.Id} and {FriendId}");
            }

            return Ok();
        }

        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid Id)
        {
            UserResponseDTO? User = await _userService.GetUserByIdAsync(Id);
            if (User == null)
            {
                return NotFound($"Error getting user with id {Id}: no such user exists.");
            }

            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRequestDTO User)
        {
            await _userService.CreateUserAsync(User);
            return Ok();
        }


        [HttpPut("login")]
        public IActionResult Login([FromBody]UserRequestDTO User)
        {
            UserResponseDTO? response = _userService.Authenticate(User);
            if (response == null)
            {
                return BadRequest($"Error logging in as \"{User.Username}\": no user found with these credentials.");
            }

            return Ok(response);
        }

        [HttpGet("servers")]
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

        [HttpPost("server/{ServerId}/{Role}")]
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

        [HttpDelete("server/{ServerId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> LeaveServer([FromRoute]Guid ServerId)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error leaving server {ServerId}: no user logged in.");
            }

            if (await _userService.LeaveServerAsync(User.Id, ServerId) == null)
            {
                return NotFound($"Error leaving server {ServerId}: no such server exists.");
            }

            return Ok();
        }
    }
}

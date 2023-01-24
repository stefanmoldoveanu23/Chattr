using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Helpers.Hubs;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
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
using BCryptNet = BCrypt.Net.BCrypt;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allUsers = await _userService.GetUsersAsync();
            return Ok(allUsers);
        }

        [HttpGet("get-friends")]
        public async Task<IActionResult> GetFriends()
        {
            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            List<UserResponseDTO> friends = (await _userService.GetFriendsAsync(UserId));
            foreach (UserResponseDTO friend in friends)
            {
                friend.Email = "";
            }

            return Ok(friends);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestDTO User)
        {
            User.Password = BCryptNet.HashPassword(User.Password);
            await _userService.CreateUserAsync(User);
            return Ok();
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRequestDTO User)
        {
            UserResponseDTO? response = _userService.Authenticate(User);
            if (response == null)
            {
                return BadRequest("User or password is wrong. Try again.");
            }

            return Ok(response);
        }

        [HttpGet("get-servers")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetServers()
        {
            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            Console.WriteLine(UserId);

            List<ServerResponseDTO> Servers = await _userService.GetServersAsync(UserId);

            return Ok(Servers);
        }

        [HttpPost("join-server/{ServerId}/{role}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> JoinServer([FromRoute]Guid ServerId , [FromRoute] Roles role)
        {
            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;

            await _userService.JoinServerAsync(UserId, ServerId, role);
            return Ok();
        }
    }
}

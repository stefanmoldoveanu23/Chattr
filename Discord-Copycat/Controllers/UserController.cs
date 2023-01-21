using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.UserService;
using Discord_Copycat.Data;
using Discord_Copycat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("get-friends/{id}")]
        public async Task<IActionResult> GetFriends([FromRoute]Guid id)
        {
            var friends = await _userService.GetFriendsAsync(id);

            return Ok(friends);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRequestDTO User)
        {
            User.Password = BCryptNet.HashPassword(User.Password);
            await _userService.CreateUserAsync(User);
            return Ok();
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody]UserRequestDTO User)
        {

            UserResponseDTO? response = _userService.Authenticate(User);
            if (response == null)
            {
                return BadRequest("User or password is wrong. Try again.");
            }

            return Ok(response);
        }
    }
}

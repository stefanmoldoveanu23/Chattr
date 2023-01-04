using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.UserService;
using Discord_Copycat.Data;
using Discord_Copycat.Models;
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

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody]UserRequestDTO User)
        {
            await _userService.CreateUserAsync(User);

            return Ok();
        }
    }
}

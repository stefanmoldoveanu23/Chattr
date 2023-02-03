using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ServerService;
using Discord_Copycat.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Discord_Copycat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly IServerService _serverService;

        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateServer([FromBody]ServerRequestDTO Server)
        {
            ServerResponseDTO newServer = await _serverService.CreateServerAsync(Server);

            return Ok(newServer);
        }

        [HttpGet("get-role/{Id}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetRole([FromRoute]Guid Id)
        {
            UserResponseDTO? User = HttpContext.Items["User"] as UserResponseDTO;
            if (User == null)
            {
                return NotFound("User not logged in.");
            }

            Roles? Role = await _serverService.GetUserRole(Id, User.Id);
            if (Role == null)
            {
                return BadRequest($"User {User.Id} not in server {Id}.");
            }

            return Ok(Role);
        }

        [HttpGet("get-chats-for-user/{Id}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetChatsForUser([FromRoute]Guid Id)
        {
            UserResponseDTO? User = HttpContext.Items["User"] as UserResponseDTO;
            if (User == null)
            {
                return NotFound("User not logged in.");
            }

            List<ChatResponseDTO>? Chats = await _serverService.GetChatsForUserAsync(Id, User.Id);
            if (Chats == null)
            {
                return BadRequest($"User {User.Id} not in server ${Id}.");
            }

            return Ok(Chats);
        }

        [HttpGet("get-chats/{Id}")]
        public async Task<IActionResult> GetChats([FromRoute]Guid Id)
        {
            List<ChatResponseDTO>? Chats = await _serverService.GetChatsAsync(Id);
            if (Chats == null)
            {
                return NotFound($"Server with id {Id} doesn't exist.");
            }

            return Ok(Chats);
        }

        [HttpGet("get-members/{Id}")]
        public async Task<IActionResult> GetMembers([FromRoute]Guid Id)
        {
            List<UserResponseDTO>? Users = await _serverService.GetUsersAsync(Id);
            if (Users == null)
            {
                return NotFound($"Server with id ${Id} doesn't exist.");
            }

            return Ok(Users);
        }

        [HttpGet("get-server/{Id}")]
        public async Task<IActionResult> GetServer([FromRoute]Guid Id)
        {
            ServerResponseDTO? Server = await _serverService.GetServerByIdAsync(Id);
            if (Server == null)
            {
                return NotFound($"Server with id {Id} doesn't exist.");
            }

            return Ok(Server);
        }
    }
}

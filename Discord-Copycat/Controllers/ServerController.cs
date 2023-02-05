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
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error getting role of user in server with id {Id}: no user logged in.");
            }

            Roles? Role = await _serverService.GetUserRole(Id, User.Id);
            if (Role == null)
            {
                return NotFound($"Error getting role of user in server with id {Id}: user {User.Id} is not member of this server.");
            }

            return Ok(Role);
        }

        [HttpGet("get-chats-for-user/{Id}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetChatsForUser([FromRoute]Guid Id)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error getting chats for user in server with id {Id}: no user logged in.");
            }

            List<ChatResponseDTO>? Chats = await _serverService.GetChatsForUserAsync(Id, User.Id);
            if (Chats == null)
            {
                return NotFound($"Error getting chats for user in server with {Id}: user {User.Id} is not member of this server.");
            }

            return Ok(Chats);
        }

        [HttpGet("get-chats/{Id}")]
        public async Task<IActionResult> GetChats([FromRoute]Guid Id)
        {
            List<ChatResponseDTO>? Chats = await _serverService.GetChatsAsync(Id);
            if (Chats == null)
            {
                return NotFound($"Error getting chats of server with id {Id}: no such server exists.");
            }

            return Ok(Chats);
        }

        [HttpGet("get-members/{Id}")]
        public async Task<IActionResult> GetMembers([FromRoute]Guid Id)
        {
            List<UserResponseDTO>? Users = await _serverService.GetUsersAsync(Id);
            if (Users == null)
            {
                return NotFound($"Error getting member of server with id ${Id}: no such server exists.");
            }

            return Ok(Users);
        }

        [HttpGet("get-server/{Id}")]
        public async Task<IActionResult> GetServer([FromRoute]Guid Id)
        {
            ServerResponseDTO? Server = await _serverService.GetServerByIdAsync(Id);
            if (Server == null)
            {
                return NotFound($"Error getting server with id {Id}: no such server exists.");
            }

            return Ok(Server);
        }

        [HttpGet("get-server")]
        public ServerResponseDTO? GetServerFromToken()
        {
            return HttpContext.Items["Server"] as ServerResponseDTO;
        }

        [HttpGet("get-server-token/{Id}")]
        public async Task<IActionResult> GetServerToken([FromRoute]Guid Id)
        {
            string? token = await _serverService.GetServerToken(Id);
            if (token == null)
            {
                return NotFound($"Error getting server join link for server with id {Id}: no such server exists.");
            }

            Console.WriteLine(HttpContext.Request.Path);

            return Ok(new { token });
        }
    }
}

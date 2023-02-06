using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ServerService;
using Discord_Copycat.Models;
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

        [HttpPost()]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> CreateServer([FromBody] ServerRequestDTO Server)
        {
            ServerResponseDTO newServer = await _serverService.CreateServerAsync(Server);

            return Ok(newServer);
        }

        [HttpGet("{Id}/role")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetRole([FromRoute] Guid Id)
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

        [HttpGet("{Id}/chats-for-user")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetChatsForUser([FromRoute] Guid Id)
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

        [HttpGet("{Id}/chats")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetChats([FromRoute] Guid Id)
        {
            List<ChatResponseDTO>? Chats = await _serverService.GetChatsAsync(Id);
            if (Chats == null)
            {
                return NotFound($"Error getting chats of server with id {Id}: no such server exists.");
            }

            return Ok(Chats);
        }

        [HttpGet("{Id}/members")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetMembers([FromRoute] Guid Id)
        {
            List<UserResponseDTO>? Users = await _serverService.GetUsersAsync(Id);
            if (Users == null)
            {
                return NotFound($"Error getting member of server with id ${Id}: no such server exists.");
            }

            return Ok(Users);
        }

        [HttpGet("by-id/{Id}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetServer([FromRoute] Guid Id)
        {
            ServerResponseDTO? Server = await _serverService.GetServerByIdAsync(Id);
            if (Server == null)
            {
                return NotFound($"Error getting server with id {Id}: no such server exists.");
            }

            return Ok(Server);
        }

        [HttpGet("by-token")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public ServerResponseDTO? GetServerFromToken()
        {
            return HttpContext.Items["Server"] as ServerResponseDTO;
        }

        [HttpGet("{Id}/token")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetServerToken([FromRoute] Guid Id)
        {
            string? token = await _serverService.GetServerToken(Id);
            if (token == null)
            {
                return NotFound($"Error getting server join link for server with id {Id}: no such server exists.");
            }

            Console.WriteLine(HttpContext.Request.Path);

            return Ok(new { token });
        }

        [HttpDelete("{Id}/admin")]
        [Authorization(Roles.Admin)]
        public async Task<IActionResult> DeleteServerNotOwned([FromRoute]Guid Id)
        {
            if (await _serverService.GetServerByIdAsync(Id) == null)
            {
                return NotFound($"Error deleting server with id {Id}: no such server exists.");
            }

            _serverService.DeleteServer(new Server { Id = Id });
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> DeleteServer([FromRoute]Guid Id)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest($"Error deleting server with id {Id}: no user logged in.");
            }

            if (await _serverService.GetUserRole(Id, User.Id) is not Roles Role || Role < Roles.Admin)
            {
                return BadRequest($"Error deleting server with id {Id}: you don't have the authorization to do this.");
            }

            _serverService.DeleteServer(new Server { Id = Id });
            return Ok();
        }
    }
}

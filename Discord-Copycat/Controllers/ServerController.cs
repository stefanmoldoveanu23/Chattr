using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ServerService;
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

        [HttpGet("get-chats/{Id}")]
        public async Task<IActionResult> GetChats([FromRoute]Guid Id)
        {
            List<ChatResponseDTO>? Chats = await _serverService.GetChatsAsync(Id);
            if (Chats == null)
            {
                return NotFound();
            }

            return Ok(Chats);
        }

        [HttpGet("get-members/{Id}")]
        public async Task<IActionResult> GetMembers([FromRoute]Guid Id)
        {
            List<UserResponseDTO>? Users = (await _serverService.GetUsersAsync(Id));
            if (Users == null)
            {
                return NotFound();
            }

            return Ok(Users);
        }
    }
}

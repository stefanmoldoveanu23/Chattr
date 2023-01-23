using ClassLibrary.Models.DTOs.ServerDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ServerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("get-members/{Id}")]
        public async Task<IActionResult> GetMembers([FromRoute]Guid Id)
        {
            List<UserResponseDTO> users = (await _serverService.GetUsersAsync(Id));

            return Ok(users);
        }
    }
}

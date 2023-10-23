using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ChatService;
using ClassLibrary.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chattr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("{ServerId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> Create([FromRoute]Guid ServerId, [FromBody]ChatRequestDTO Chat)
        {
            Chat.ServerId = ServerId;
            ChatResponseDTO ChatResult = await _chatService.CreateChatAsync(Chat);

            return Ok(ChatResult);
        }

        [HttpGet("{ChatId}/users")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> GetUsers([FromRoute]Guid ChatId)
        {
            List<UserResponseDTO>? Users = await _chatService.GetUsersAsync(ChatId);
            if (Users == null)
            {
                return NotFound($"Error getting users: chat with id {ChatId} not found.");
            }

            return Ok(Users);
        }

        [HttpPost("{ChatId}/log")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> SendMessage([FromRoute]Guid ChatId, [FromBody]LogRequestDTO Message)
        {
            if (HttpContext.Items["User"] is not UserResponseDTO User)
            {
                return BadRequest("Error sending message: no user logged in.");
            }

            LogResponseDTO? Log = await _chatService.SendMessage(ChatId, User.Id, Message.Message);
            if (Log == null)
            {
                return NotFound($"Error sending message: chat with id {ChatId} not found.");
            }

            return Ok(Log);
        }

        [HttpDelete("{ChatId}/log/{LogId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> DeleteMessage([FromRoute]Guid ChatId, [FromRoute]Guid LogId)
        {
            if (HttpContext.Items["User"] is null)
            {
                return BadRequest("Error deleting message: no user logged in.");
            }

            if (await _chatService.DeleteMessage(ChatId, LogId) == null)
            {
                return NotFound($"Error deleting message: log with id {LogId} not found in chat with id {ChatId}.");
            }

            return Ok();
        }

        [HttpGet("{ChatId}/logs")]
        public async Task<IActionResult> GetLogs([FromRoute]Guid ChatId)
        {
            List<LogResponseDTO>? Logs = await _chatService.GetLogsAsync(ChatId);
            if (Logs == null)
            {
                return NotFound($"Error getting logs: chat with id ${ChatId} not found.");
            }

            return Ok(Logs);
        }
    }
}

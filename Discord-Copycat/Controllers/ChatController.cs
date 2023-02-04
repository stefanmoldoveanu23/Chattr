using ClassLibrary.Helpers.Attributes;
using ClassLibrary.Models.DTOs.ChatDTO;
using ClassLibrary.Models.DTOs.LogDTO;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ChatService;
using Discord_Copycat.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discord_Copycat.Controllers
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

        [HttpPost("create/{ServerId}")]
        public async Task<IActionResult> Create([FromRoute]Guid ServerId, [FromBody]ChatRequestDTO Chat)
        {
            Chat.ServerId = ServerId;
            ChatResponseDTO ChatResult = await _chatService.CreateChatAsync(Chat);

            return Ok(ChatResult);
        }

        [HttpGet("get-users/{ChatId}")]
        public async Task<IActionResult> GetUsers([FromRoute]Guid ChatId)
        {
            List<UserResponseDTO>? Users = await _chatService.GetUsersAsync(ChatId);
            if (Users == null)
            {
                return NotFound();
            }

            return Ok(Users);
        }

        [HttpPut("send-message/{ChatId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> SendMessage([FromRoute]Guid ChatId, [FromBody]LogRequestDTO Message)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            LogResponseDTO? Log = await _chatService.SendMessage(ChatId, UserId, Message.Message);
            if (Log == null)
            {
                return NotFound();
            }

            return Ok(Log);
        }

        [HttpGet("get-logs/{ChatId}")]
        public async Task<IActionResult> GetLogs([FromRoute]Guid ChatId)
        {
            List<LogResponseDTO>? Logs = await _chatService.GetLogsAsync(ChatId);
            if (Logs == null)
            {
                return NotFound();
            }

            return Ok(Logs);
        }
    }
}

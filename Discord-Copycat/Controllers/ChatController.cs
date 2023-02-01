using ClassLibrary.Helpers.Attributes;
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

        [HttpGet("get-users/{chatId}")]
        public async Task<IActionResult> GetUsers([FromRoute]Guid chatId)
        {
            List<UserResponseDTO>? Users = await _chatService.GetUsersAsync(chatId);
            if (Users == null)
            {
                return NotFound();
            }

            return Ok(Users);
        }

        [HttpPut("send-message/{chatId}")]
        [Authorization(Roles.Admin, Roles.Mod, Roles.User)]
        public async Task<IActionResult> SendMessage([FromRoute]Guid chatId, [FromBody]string Message)
        {
            if (HttpContext.Items["User"] == null)
            {
                return NotFound();
            }

            Guid UserId = (HttpContext.Items["User"] as UserResponseDTO).Id;
            LogResponseDTO? Log = await _chatService.SendMessage(chatId, UserId, Message);
            if (Log == null)
            {
                return NotFound();
            }

            return Ok(Log);
        }

        [HttpGet("get-logs/{chatId}")]
        public async Task<IActionResult> GetLogs([FromRoute]Guid chatId)
        {
            List<LogResponseDTO>? Logs = await _chatService.GetLogsAsync(chatId);
            if (Logs == null)
            {
                return NotFound();
            }

            return Ok(Logs);
        }
    }
}

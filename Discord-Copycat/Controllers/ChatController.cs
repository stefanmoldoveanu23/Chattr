using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ChatService;
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
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        // POST: api/chat
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Unauthorized("User ID not found in token.");

            var response = await _chatService.GetAiResponseAsync(userId, message);

            return Ok(new { response });
        }

        // GET: api/chat/history
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int limit = 20)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Unauthorized("User ID not found in toiken.");

            var messages = await _chatService.GetChatHistoryAsync(userId, limit);

            var result = messages
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    m.Role,
                    m.Message,
                    Timestamp = m.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
                });

            return Ok(result);
        }
    }
}

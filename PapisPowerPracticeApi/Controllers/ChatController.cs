using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Services;

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

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _chatService.GetAiResponseAsync(userId, message);

            return Ok(new { response });
        }
    }
}

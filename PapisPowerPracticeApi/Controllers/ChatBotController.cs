using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.DTOs.Chat.Request;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok("ChatBot controller is working");
        }

        // POST /api/ChatBot/chat
        [HttpPost("chat")]
        public async Task<ActionResult> Chat([FromBody] ChatRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Message))
            {
                return BadRequest("Message cannot be empty");
            }

            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var assistantMessage = await _chatBotService.SendMessageAsync(userId, request);
                return Ok(assistantMessage);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET /api/ChatBot/{sessionId}
        [HttpGet("{sessionId:guid}")]
        public async Task<ActionResult> GetSessionMessages(Guid sessionId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var messages = await _chatBotService.GetSessionMessagesAsync(sessionId, userId);
                return Ok(messages);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string? GetUserId()
        {
            return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User?.FindFirst("sub")?.Value
                ?? User?.Identity?.Name;
        }
    }
}
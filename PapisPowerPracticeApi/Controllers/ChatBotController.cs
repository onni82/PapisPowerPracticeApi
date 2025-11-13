using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("chat")]
        public async Task<ActionResult<string>> Chat([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
                return BadRequest("Message cannot be empty");
                
            try
            {
                var response = await _chatBotService.GetResponseAsync(message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
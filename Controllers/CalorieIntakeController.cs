using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;  

namespace PapisPowerPracticeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalorieIntakeController : ControllerBase
    {
        private readonly ICalorieCalculatorService _service;
        
        public CalorieIntakeController(ICalorieCalculatorService service)
        {
            _service = service;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateCalories([FromBody] CalorieData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.Calculate(data);
            return Ok(result);
        }

        [HttpGet("activity-levels")]
        public IActionResult GetActivityLevels()
        {
            return Ok(_service.GetActivityLevels());
        }
    }
}
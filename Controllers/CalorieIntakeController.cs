using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services;

namespace PapisPowerPracticeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalorieIntakeController : ControllerBase
    {
        private readonly CalorieCalculatorService _service;

        // Service is injected via Dependency Injection (Program.cs)
        public CalorieIntakeController(CalorieCalculatorService service)
        {
            _service = service;
        }

        // POST: api/calorieintake/calculate
        [HttpPost("calculate")]
        public IActionResult CalculateCalories([FromBody] CalorieData data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.Calculate(data);
            return Ok(result);
        }

        // GET: api/calorieintake/activity-levels
        [HttpGet("activity-levels")]
        public IActionResult GetActivityLevels()
        {
            return Ok(_service.GetActivityLevels());
        }
    }
}
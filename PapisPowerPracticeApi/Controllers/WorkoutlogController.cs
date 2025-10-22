using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Request;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Response;
using PapisPowerPracticeApi.DTOs.WorkoutLog;
using PapisPowerPracticeApi.Services.IServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PapisPowerPracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutLogController : ControllerBase
    {
        private readonly IWorkoutLogService _workoutLogService;

        public WorkoutLogController(IWorkoutLogService workoutLogService)
        {
            _workoutLogService = workoutLogService;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetWorkoutLogDTO>>> GetAllWorkoutLogsAsync()
        {
            var workoutLog = await _workoutLogService.GetAllWorkoutLogsAsync();
            return Ok(workoutLog);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateWorkoutLog(CreateWorkoutLogDTO workoutLogDTO)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                         User.FindFirst("sub")?.Value ??
                         User.FindFirst("id")?.Value ??
                         User.FindFirst("userId")?.Value ??
                         User.FindFirst("nameid")?.Value ??
                         User.Identity?.Name;

            if (string.IsNullOrEmpty(userId))
                return BadRequest($"User ID not found. Available claims: {string.Join(", ", claims.Select(c => $"{c.Type}:{c.Value}"))}");

            var result = await _workoutLogService.CreateWorkoutLogAsync(workoutLogDTO, userId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetWorkoutLogDTO>> GetWorkoutLogById(int id)
        {
            var workoutLog = await _workoutLogService.GetWorkoutLogByIdAsync(id);
            return Ok(workoutLog);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GetWorkoutLogDTO>> UpdateWorkoutLog(GetWorkoutLogDTO WorkoutLogDTO)
        {
            var workoutLog = await _workoutLogService.UpdateWorkoutLogAsync( WorkoutLogDTO);
            return Ok(workoutLog);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GetWorkoutLogDTO>> DeleteWorkoutLog(int id)
        {
            var workoutLog = await _workoutLogService.DeleteWorkoutLogAsync(id);
            return Ok(workoutLog);
        }
    }
}

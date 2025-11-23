using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Request;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Response;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseController : ControllerBase
    {
        private readonly IWorkoutExerciseService _workoutService;

        public WorkoutExerciseController(IWorkoutExerciseService workoutService)
        {
            _workoutService = workoutService;
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateWorkoutExercise(CreateWorkoutExerciseDTO exerciseDTO)
        //{
        //    var workout = await _workoutService.CreateWorkoutExerciseAsync(exerciseDTO);
        //    return Ok(workout);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExerciseDTO>> GetWorkoutExerciseById(int id)
        {
            var workout = await _workoutService.GetWorkoutExerciseByIdAsync(id);
            if(workout == null)
            {
                return NotFound();
            }
            return Ok(workout);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutExercise(int id)
        {
            var workout = await _workoutService.DeleteWorkoutExerciseAsync(id);
            if (!workout)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

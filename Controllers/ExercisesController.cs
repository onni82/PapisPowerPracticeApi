using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExercisesController(IExerciseService service)
        {
            _service = service;
        }

        // GET: api/exercises
        [HttpGet]
        public async Task<IActionResult> GetAllExercise()
        {
            var exercise = await _service.GetAllExercisesAsync();

            var result = exercise.Select(e => new
            {
                e.Id,
                e.Name,
                e.Description,
                e.VideoUrl,
                MuscleGroups = e.MuscleGroups.Select(m => m.Name).ToList()
            });

            return Ok(result);
        }

        // GET: api/exercises/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            var exercise = await _service.GetExerciseByIdAsync(id);
            if (exercise == null) return NotFound();

            return Ok(new
            {
                exercise.Id,
                exercise.Name,
                exercise.Description,
                exercise.VideoUrl,
                MuscleGroups = exercise.MuscleGroups.Select(m => m.Name).ToList()
            });
        }

        // POST: api/exercises
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateExerciseAsync(exercise);
            return CreatedAtAction(nameof(GetExerciseById), new { id = created.Id }, created);
        }

        // PUT: api/exercises/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] Exercise exercise)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateExerciseAsync(id, exercise);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // DELETE: api/exercises/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var deleted = await _service.DeleteExerciseAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

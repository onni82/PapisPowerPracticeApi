using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.DTOs.Exercise.Request;
using PapisPowerPracticeApi.DTOs.Exercise.Response;
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

            var result = exercise.Select(e => new ExerciseDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                VideoUrl = e.VideoUrl,
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

            return Ok(new ExerciseDTO
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                VideoUrl = exercise.VideoUrl,
                MuscleGroups = exercise.MuscleGroups.Select(m => m.Name).ToList()
            });
        }

        // POST: api/exercises
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exercise = new Exercise
            {
                Name = dto.Name,
                Description = dto.Description,
                VideoUrl = dto.VideoUrl ?? string.Empty,
                MuscleGroups = dto.MuscleGroupIds.Select(id => new MuscleGroup { Id = id }).ToList()
            };



            var created = await _service.CreateExerciseAsync(exercise);

            var result = new ExerciseDTO
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                VideoUrl = created.VideoUrl,
                MuscleGroups = created.MuscleGroups.Select(m => m.Name).ToList()
            };

            return CreatedAtAction(nameof(GetExerciseById), new { id = result.Id }, result);
        }

        // PUT: api/exercises/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] ExerciseCreateUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exercise = new Exercise
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                VideoUrl = dto.VideoUrl ?? string.Empty,
                MuscleGroups = dto.MuscleGroupIds.Select(id => new MuscleGroup { Id = id }).ToList()
            };

            var updated = await _service.UpdateExerciseAsync(id, exercise);
            if (updated == null) return NotFound();

            var result = new ExerciseDTO
            {
                Id = updated.Id,
                Name = updated.Name,
                Description = updated.Description,
                VideoUrl = updated.VideoUrl,
                MuscleGroups = updated.MuscleGroups.Select(m => m.Name).ToList()
            };

            return Ok(result);
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

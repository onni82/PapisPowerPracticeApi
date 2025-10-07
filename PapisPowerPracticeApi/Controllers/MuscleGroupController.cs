using Microsoft.AspNetCore.Mvc;
using PapisPowerPracticeApi.DTOs;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuscleGroupController : ControllerBase
    {
        private readonly IMuscleGroupService _muscleGroupService;

        // GET api/<ReservationController>/5
        public MuscleGroupController(IMuscleGroupService muscleGroupService)
        {
            _muscleGroupService = muscleGroupService;
        }
        [HttpGet]
        public async Task<ActionResult<List<MuscleGroupDTO>>> GetAllMuscleGroups()
        {
            var muscleGroups = await _muscleGroupService.GetAllMuscleGroupsAsync();
            return Ok(muscleGroups);
        }

        // POST api/<ReservationController>
        [HttpPost]
        public async Task<ActionResult<int>> AddMuscleGroup(CreateMuscleGroupDTO createMuscleGroupDTO)
        {
            var muscleGroupDTO = new MuscleGroupDTO
            {
                Name = createMuscleGroupDTO.Name,
                ImageUrl = createMuscleGroupDTO.ImageUrl
            };
            var muscleGroupId = await _muscleGroupService.AddMuscleGroupAsync(muscleGroupDTO);
            return Ok(muscleGroupId);
        }
        [HttpGet("{muscleGroupId}")]
        public async Task<ActionResult<MuscleGroupDTO>> GetMuscleGroupById(int muscleGroupId)
        {
            var muscleGroup = await _muscleGroupService.GetMuscleGroupByIdAsync(muscleGroupId);
            return Ok(muscleGroup);
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MuscleGroupDTO>> UpdateMuscleGroupAsync(int id, MuscleGroupDTO muscleGroupDTO)
        {
            var muscleGroup = await _muscleGroupService.UpdateMuscleGroupAsync(muscleGroupDTO);
            return Ok(muscleGroup);
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{muscleGroupId}")]
        public async Task<ActionResult<MuscleGroup>> DeleteMuscleGroup(int muscleGroupId)
        {
            var muscleGroup = await _muscleGroupService.DeleteMuscleGroupAsync(muscleGroupId);
            return Ok(muscleGroup);

        }
    }
}

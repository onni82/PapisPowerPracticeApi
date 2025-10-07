using PapisPowerPracticeApi.DTOs;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class MuscleGroupService : IMuscleGroupService
    {
        private readonly IMuscleGroupRepository _muscleGroupRepository;
        public MuscleGroupService(IMuscleGroupRepository muscleGroupRepository)
        {
            _muscleGroupRepository = muscleGroupRepository;
        }

        public async Task<List<MuscleGroupDTO>> GetAllMuscleGroupsAsync()
        {
            var muscleGroups = await _muscleGroupRepository.GetAllMuscleGroupsAsync();

            var muscleGroupDTO = muscleGroups.Select(r => new MuscleGroupDTO
            {
                Id = r.Id,
                Name = r.Name,
                ImageUrl = r.ImageUrl,
            }).ToList();
            return muscleGroupDTO;
        }
        public async Task<MuscleGroupDTO> GetMuscleGroupByIdAsync(int muscleGroupId)
        {
            var muscleGroup = await _muscleGroupRepository.GetMuscleGroupByIdAsync(muscleGroupId);
            if (muscleGroup == null)
            {
                return null;
            }
            var muscleGroupDTO = new MuscleGroupDTO
            {
                Id = muscleGroup.Id,
                Name = muscleGroup.Name,
                ImageUrl = muscleGroup.ImageUrl,
            };
            return muscleGroupDTO;
        }
        public async Task<int> AddMuscleGroupAsync(MuscleGroupDTO muscleGroupDTO)
        {
            var muscleGroup = new MuscleGroup
            {
                Name = muscleGroupDTO.Name,
                ImageUrl = muscleGroupDTO.ImageUrl,
            };
            var addedMuscleGroup = await _muscleGroupRepository.AddMuscleGroupAsync(muscleGroup);
            return addedMuscleGroup;
        }
        public async Task<bool> UpdateMuscleGroupAsync(MuscleGroupDTO muscleGroupDTO)
        {
            var muscleGroup = new MuscleGroup
            {
                Id = muscleGroupDTO.Id,
                Name = muscleGroupDTO.Name,
                ImageUrl = muscleGroupDTO.ImageUrl,
            };
            var updatedMuscleGroup = await _muscleGroupRepository.UpdateMuscleGroupAsync(muscleGroup);
            return updatedMuscleGroup;
        }
        public Task<bool> DeleteMuscleGroupAsync(int muscleGroupId)
        {
            var deletedMuscleGroup = _muscleGroupRepository.DeleteMuscleGroupAsync(muscleGroupId);
            return deletedMuscleGroup;
        }
    }
}

using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.DTOs;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IMuscleGroupService
    {
        Task<List<MuscleGroupDTO>> GetAllMuscleGroupsAsync();
        Task<MuscleGroupDTO> GetMuscleGroupByIdAsync(int muscleGroupId);
        Task<int> AddMuscleGroupAsync(MuscleGroupDTO muscleGroupDTO);
        Task<bool> UpdateMuscleGroupAsync(MuscleGroupDTO muscleGroupDTO);
        Task<bool> DeleteMuscleGroupAsync(int muscleGroupId);
    }
}

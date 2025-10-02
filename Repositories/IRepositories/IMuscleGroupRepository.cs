namespace PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Models;

public interface IMuscleGroupRepository
{
    public interface IMenuRepository
    {
        Task<List<MuscleGroup>> GetAllMuscleGroupsAsync();
        Task<MuscleGroup> GetMuscleGroupByIdAsync(int muscleGroupId);
        Task<int> AddMuscleGroupAsync(MuscleGroup muscleGroup);
        Task<bool> UpdateMuscleGroupAsync(MuscleGroup muscleGroup);
        Task<bool> DeleteMuscleGroupAsync(int muscleGroupId);
    }
}


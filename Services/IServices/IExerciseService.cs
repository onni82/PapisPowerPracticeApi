using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IExerciseService
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise?> GetExerciseByIdAsync(int id);
        Task<Exercise> CreateExerciseAsync(Exercise exercise);
        Task<Exercise?> UpdateExerciseAsync(int id, Exercise exercise);
        Task<bool> DeleteExerciseAsync(int id);
    }
}

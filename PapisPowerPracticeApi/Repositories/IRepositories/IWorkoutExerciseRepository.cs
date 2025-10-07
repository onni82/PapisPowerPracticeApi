using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Repositories.IRepositories
{
    public interface IWorkoutExerciseRepository
    {
        Task<List<WorkoutExercise>> GetAllWorkoutExercisesAsync();
        Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(int id);
        Task<int> AddWorkoutExerciseAsync(WorkoutExercise exercise);
        Task<bool> UpdateWorkoutExerciseAsync(WorkoutExercise exercise);
        Task<bool> DeleteWorkoutExerciseAsync(int id);
    }
}

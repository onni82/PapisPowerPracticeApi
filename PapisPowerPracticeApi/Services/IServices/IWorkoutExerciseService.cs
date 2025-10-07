using PapisPowerPracticeApi.DTOs.WorkoutExercise.Request;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Response;
using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IWorkoutExerciseService
    {
        Task<WorkoutExerciseDTO> GetWorkoutExerciseByIdAsync(int id);
        Task<int> CreateWorkoutExerciseAsync(CreateWorkoutExerciseDTO createWorkout);
        Task<bool> UpdateWorkoutExerciseAsync(int id,PatchWorkoutExerciseDTO patchWorkout);
        Task<bool> DeleteWorkoutExerciseAsync(int id);
    }
}

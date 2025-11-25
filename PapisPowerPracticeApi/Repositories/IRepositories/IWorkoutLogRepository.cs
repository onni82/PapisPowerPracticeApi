using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Repositories.IRepositories
{
    public interface IWorkoutLogRepository
    {
        Task<List<WorkoutLog>> GetAllWorkoutLogsAsync();
        Task<List<WorkoutLog>> GetWorkoutLogByUserAsync(string UserId);
        Task<WorkoutLog> GetWorkoutLogByIdAsync(int id);
        Task<int> AddWorkoutLogAsync(WorkoutLog workoutlog);
        Task<bool> UpdateWorkoutLogAsync(WorkoutLog workoutlog);
        Task<bool> DeleteWorkoutLogAsync(int id);
    }
}

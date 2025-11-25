using PapisPowerPracticeApi.DTOs;
using PapisPowerPracticeApi.DTOs.WorkoutLog;

namespace PapisPowerPracticeApi.Services.IServices
{
    public interface IWorkoutLogService
    {
        Task<List<GetWorkoutLogDTO>> GetAllWorkoutLogsAsync();
        Task<List<GetWorkoutLogDTO>> GetWorkoutLogByUserAsync(string UserId);
        Task<GetWorkoutLogDTO> GetWorkoutLogByIdAsync(int workoutLogid);
        Task<int> CreateWorkoutLogAsync(CreateWorkoutLogDTO createWorkoutLogDTO, string userId);
        Task<bool> DeleteWorkoutLogAsync(int workoutLogId);
        Task<bool> UpdateWorkoutLogAsync(GetWorkoutLogDTO getworkoutLogDTO);
    }
}

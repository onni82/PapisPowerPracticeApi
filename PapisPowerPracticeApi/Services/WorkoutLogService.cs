using PapisPowerPracticeApi.DTOs;
using PapisPowerPracticeApi.DTOs.WorkoutLog;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class WorkoutLogService : IWorkoutLogService
    {
        private readonly IWorkoutLogRepository _workoutLogRepository;
        public WorkoutLogService(IWorkoutLogRepository workoutLogRepository)
        {
            _workoutLogRepository = workoutLogRepository;
        }

        public async Task<List<GetWorkoutLogDTO>> GetAllWorkoutLogsAsync()
        {
            var workoutLog = await _workoutLogRepository.GetAllWorkoutLogsAsync();

            var getWorkoutLogDTO = workoutLog.Select(r => new GetWorkoutLogDTO
            {
                Id = r.Id,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Notes = r.Notes,
            }).ToList();
            return getWorkoutLogDTO;
        }
        public async Task<GetWorkoutLogDTO> GetWorkoutLogByIdAsync(int workoutLogid)
        {
            var workoutLog = await _workoutLogRepository.GetWorkoutLogByIdAsync(workoutLogid);
            if (workoutLog == null)
            {
                return null;
            }
            var getworkoutLogDTO = new GetWorkoutLogDTO
            {
             Id = workoutLog.Id,
             StartTime = workoutLog.StartTime,
             EndTime = workoutLog.EndTime,
             Notes = workoutLog.Notes,
            };
            return getworkoutLogDTO;
        }
        public async Task<int> CreateWorkoutLogAsync(CreateWorkoutLogDTO createworkoutLogDTO, string userId)
        {
            var workoutLog = new WorkoutLog
            {
                StartTime = createworkoutLogDTO.StartTime,
                EndTime = createworkoutLogDTO.EndTime,
                Notes = createworkoutLogDTO.Notes,
                UserId = userId
            };
            var addedWorkoutLog = await _workoutLogRepository.AddWorkoutLogAsync(workoutLog);
            return addedWorkoutLog;
        }
        public async Task<bool> UpdateWorkoutLogAsync(GetWorkoutLogDTO getworkoutLogDTO)
        {
            var workoutLog = new WorkoutLog
            {
                Id = getworkoutLogDTO.Id,
                StartTime = getworkoutLogDTO.StartTime,
                EndTime = getworkoutLogDTO.EndTime,
                Notes = getworkoutLogDTO.Notes,
            };
            var updatedWorkoutLog = await _workoutLogRepository.UpdateWorkoutLogAsync(workoutLog);
            return updatedWorkoutLog;
        }
        public Task<bool> DeleteWorkoutLogAsync(int workoutLogId)
        {
            var deletedWorkoutLog = _workoutLogRepository.DeleteWorkoutLogAsync(workoutLogId);
            return deletedWorkoutLog;
        }

    }
}

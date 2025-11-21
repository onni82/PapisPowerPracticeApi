using PapisPowerPracticeApi.DTOs;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Response;
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
                Exercises = r.Exercises.Select(e => new WorkoutExerciseDTO
                {
                    Id = e.Id,
                    ExerciseId = e.ExerciseId,
                    Name = e.Exercise.Name,
                    Sets = e.Sets.Select(s => new WorkoutSetDTO
                    {
                        Id = s.Id,
                        Reps = s.Reps,
                        Weight = s.Weight,
                        IsWarmup = s.IsWarmup
                    }).ToList()
                }).ToList()
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
             Exercises = workoutLog.Exercises.Select(e => new WorkoutExerciseDTO
             {
                 Id = e.Id,
                 ExerciseId = e.ExerciseId,
                 Name = e.Exercise.Name,
                 Sets = e.Sets.Select(s => new WorkoutSetDTO
                 {
                     Id = s.Id,
                     Reps = s.Reps,
                     Weight = s.Weight,
                     IsWarmup = s.IsWarmup
                 }).ToList()
             }).ToList()
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
                UserId = userId,
                Exercises = new List<WorkoutExercise>()
            };

            foreach(var ex in createworkoutLogDTO.Exercises)
            {
                var workoutExercise = new WorkoutExercise
                {
                    ExerciseId = ex.ExerciseId,
                    Sets = ex.Sets.Select(s => new WorkoutSet
                    {
                        Reps = s.Reps,
                        Weight = s.Weight,
                        IsWarmup = s.IsWarmup
                    }).ToList()
                };
                workoutLog.Exercises.Add(workoutExercise);
            }
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

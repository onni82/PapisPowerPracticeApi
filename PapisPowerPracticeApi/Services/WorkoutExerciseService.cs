using PapisPowerPracticeApi.DTOs.WorkoutExercise.Request;
using PapisPowerPracticeApi.DTOs.WorkoutExercise.Response;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly IWorkoutExerciseRepository _exerciseRepository;

        public WorkoutExerciseService(IWorkoutExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<WorkoutExerciseDTO?> GetWorkoutExerciseByIdAsync(int id)
        {
            var workout = await _exerciseRepository.GetWorkoutExerciseByIdAsync(id);
            if(workout == null)
            {
                return null;
            }
            return new WorkoutExerciseDTO
            {
                Id = workout.Id,
                ExerciseId = workout.Exercise.Id,
                Name = workout.Exercise.Name,
                Sets = workout.Sets.Select(s => new WorkoutSetDTO
                {
                    Id = s.Id,
                    Reps = s.Reps,
                    Weight = s.Weight,
                    IsWarmup = s.IsWarmup
                }).ToList()

            };
        }
        //public async Task<int> CreateWorkoutExerciseAsync(CreateWorkoutExerciseDTO createWorkout)
        //{
        //    var workout = new WorkoutExercise
        //    {
        //        ExerciseId = createWorkout.ExerciseId,
        //        Sets = createWorkout.Sets.Select(s => new WorkoutSet
        //        {
        //            Reps = s.Reps,
        //            Weight = s.Weight,
        //            IsWarmup = s.IsWarmup
        //        }).ToList()
        //    };

        //    return await _exerciseRepository.AddWorkoutExerciseAsync(workout);
            
        //}

        public async Task<bool> DeleteWorkoutExerciseAsync(int id)
        {
            var deleteWorkout = await _exerciseRepository.DeleteWorkoutExerciseAsync(id);

            
            return deleteWorkout;
        }

        
    }
}

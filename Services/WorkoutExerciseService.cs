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

        public async Task<WorkoutExerciseDTO> GetWorkoutExerciseByIdAsync(int id)
        {
            var workout = await _exerciseRepository.GetWorkoutExerciseByIdAsync(id);
            if(workout == null)
            {
                return null;
            }
            var workoutDTO = new WorkoutExerciseDTO
            {
                Id = workout.Id,
                ExerciseId = workout.Exercise.Id,
                Name = workout.Exercise.Name,
                Sets = workout.Sets,
                Reps = workout.Reps,
                Weight = workout.Weight

            };
            return workoutDTO;
        }
        public async Task<int> AddWorkoutExerciseAsync(CreateWorkoutExerciseDTO createWorkout)
        {
            var workout = new WorkoutExercise
            {
                Sets = createWorkout.Sets,
                Reps = createWorkout.Reps,
                Weight = createWorkout.Weight
            };

            var newWorkout = await _exerciseRepository.AddWorkoutExerciseAsync(workout);
            return newWorkout;
        }

        public Task<bool> DeleteWorkoutExerciseAsync(int id)
        {
            var deleteWorkout = _exerciseRepository.DeleteWorkoutExerciseAsync(id);
            return deleteWorkout;
        }

        public async Task<bool> UpdateWorkoutExerciseAsync(int id, PatchWorkoutExerciseDTO patchWorkout)
        {
            var workout = await _exerciseRepository.GetWorkoutExerciseByIdAsync(id);
            if(workout == null)
            {
                return false;
            }
            if(patchWorkout.ExerciseId.HasValue)
            {
                workout.ExcerciseId = patchWorkout.ExerciseId.Value;
            }
            if (patchWorkout.Sets.HasValue)
            {
                workout.Sets = patchWorkout.Sets.Value;
            }
            if (patchWorkout.Reps.HasValue)
            {
                workout.Reps = patchWorkout.Reps.Value;
            }
            if (patchWorkout.Weight.HasValue)
            {
                workout.Weight = patchWorkout.Weight.Value;
            }

            return await _exerciseRepository.UpdateWorkoutExerciseAsync(workout);

        }
    }
}

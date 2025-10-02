using Microsoft.EntityFrameworkCore;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.IRepositories;

namespace PapisPowerPracticeApi.Repositories
{
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        private readonly AppDbContext _dbContext;

        public WorkoutExerciseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddWorkoutExerciseAsync(WorkoutExercise exercise)
        {
            _dbContext.WorkoutExercises.Add(exercise);
            await _dbContext.SaveChangesAsync();

            return exercise.Id;
        }

        public async Task<bool> DeleteWorkoutExerciseAsync(int id)
        {
            var rowsAffected = await _dbContext.WorkoutExercises.Where(w => w.Id == id).ExecuteDeleteAsync();

            if(rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<WorkoutExercise>> GetAllWorkoutExercisesAsync()
        {
            var workouts = await _dbContext.WorkoutExercises.ToListAsync();

            return workouts;
        }

        public async Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(int id)
        {
            var workout = await _dbContext.WorkoutExercises.FirstOrDefaultAsync(w => w.Id == id);

            return workout;
        }

        public async Task<bool> UpdateWorkoutExerciseAsync(WorkoutExercise exercise)
        {
            _dbContext.WorkoutExercises.Update(exercise);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}

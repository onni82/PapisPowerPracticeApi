using Microsoft.EntityFrameworkCore;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.IRepositories;

namespace PapisPowerPracticeApi.Repositories
{
    public class WorkoutLogRepository : IWorkoutLogRepository
    {
        private readonly AppDbContext _dbContext;

        public WorkoutLogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddWorkoutLogAsync(WorkoutLog workoutlog)
        {
            _dbContext.WorkoutLogs.Add(workoutlog);
            await _dbContext.SaveChangesAsync();

            return workoutlog.Id;
        }

        public async Task<bool> DeleteWorkoutLogAsync(int id)
        {
            var rowsAffected = await _dbContext.WorkoutLogs.Where(w => w.Id == id).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<WorkoutLog>> GetAllWorkoutLogsAsync()
        {
            return await _dbContext.WorkoutLogs
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Exercise)
                .ToListAsync();
        }

        public async Task<List<WorkoutLog>> GetWorkoutLogByUserAsync(string UserId)
        {
            return await _dbContext.WorkoutLogs
                .Where(w => w.UserId == UserId)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .Include(e => e.Exercises)
                .ThenInclude(e => e.Exercise)
                .ToListAsync();
        }

        public async Task<WorkoutLog> GetWorkoutLogByIdAsync(int id)
        {
            return await _dbContext.WorkoutLogs
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Sets)
                .Include(w => w.Exercises)
                .ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> UpdateWorkoutLogAsync(WorkoutLog workoutlog)
        {
            _dbContext.WorkoutLogs.Update(workoutlog);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}

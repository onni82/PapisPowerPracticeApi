using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Repositories
{
    public class MuscleGroupRepository : IMuscleGroupRepository
    {
        private readonly AppDbContext _dbContext;

        public MuscleGroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddMuscleGroupAsync(MuscleGroup muscleGroup)
        {
            _dbContext.MuscleGroups.Add(muscleGroup);
            await _dbContext.SaveChangesAsync();

            return muscleGroup.Id;

        }
        public async Task<bool> DeleteMuscleGroupAsync(int muscleGroupId)
        {
            var rowsAffected = await _dbContext.MuscleGroups.Where(s => s.Id == muscleGroupId).ExecuteDeleteAsync();

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<MuscleGroup>> GetAllMuscleGroupsAsync()
        {
            var muscleGroups = await _dbContext.MuscleGroups.ToListAsync();

            return muscleGroups;
        }
        public async Task<MuscleGroup> GetMuscleGroupByIdAsync(int muscleGroupId)
        {
            var muscleGroup = await _dbContext.MuscleGroups
                .FirstOrDefaultAsync(s => s.Id == muscleGroupId);

            return muscleGroup;

        }
        public async Task<bool> UpdateMuscleGroupAsync(MuscleGroup muscleGroup)
        {
            _dbContext.MuscleGroups.Update(muscleGroup);
            var result = await _dbContext.SaveChangesAsync();

            if (result != 0)
            {
                return true;
            }
            return false;
        }
    }
}
using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Repositories.Interfaces
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllAsync();
        Task<Exercise?> GetByIdAsync(int id);
        Task AddAsync(Exercise exercise);
        Task UpdateAsync(Exercise exercise);
        Task DeleteAsync(Exercise exercise);
        Task SaveChangesAsync();
    }
}

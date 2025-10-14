using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories.Interfaces;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _repository;
        private readonly IMuscleGroupRepository _muscleGroupRepository;

        public ExerciseService(IExerciseRepository repository, IMuscleGroupRepository muscleGroupRepository)
        {
            _repository = repository;
            _muscleGroupRepository = muscleGroupRepository;
        }


        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Exercise?> GetExerciseByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Exercise> CreateExerciseAsync(Exercise exercise)
        {
            var muscleGroups = new List<MuscleGroup>();

            foreach (var mg in exercise.MuscleGroups)
            {
                var existingMuscleGroup = await _muscleGroupRepository.GetMuscleGroupByIdAsync(mg.Id);
                if(existingMuscleGroup != null)
                {
                    muscleGroups.Add(existingMuscleGroup);
                }
                else
                {
                    throw new ArgumentException($"Musclesgroup with id {mg.Id} does not exist. ");
                }

            }

            exercise.MuscleGroups = muscleGroups;

            await _repository.AddAsync(exercise);
            await _repository.SaveChangesAsync();
            return exercise;
        }

        public async Task<Exercise?> UpdateExerciseAsync(int id, Exercise exercise)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = exercise.Name;
            existing.Description = exercise.Description;
            existing.VideoUrl = exercise.VideoUrl;
            existing.MuscleGroups = exercise.MuscleGroups;

            await _repository.UpdateAsync(existing);
            await _repository.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteExerciseAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}

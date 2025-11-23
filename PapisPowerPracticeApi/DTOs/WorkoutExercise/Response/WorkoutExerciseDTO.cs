using System.ComponentModel.DataAnnotations;

namespace PapisPowerPracticeApi.DTOs.WorkoutExercise.Response
{
    public class WorkoutExerciseDTO
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<WorkoutSetDTO> Sets { get; set; } = new();
       
    }
    public class WorkoutSetDTO
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public bool IsWarmup { get; set; }
    }
}

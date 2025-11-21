using Microsoft.AspNetCore.Authentication.Cookies;

namespace PapisPowerPracticeApi.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        
        public int WorkoutLogId { get; set; }
        public WorkoutLog WorkoutLog { get; set; } = null!;
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public ICollection<WorkoutSet> Sets { get; set; } = new List<WorkoutSet>();
    }

    public class WorkoutSet
    {
        public int Id { get; set; }
        public int WorkoutExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; } = null!;
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public bool IsWarmup { get; set; }
    }
}

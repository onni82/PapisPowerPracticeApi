namespace PapisPowerPracticeApi.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        
        public int WorkoutLogId { get; set; }
        public WorkoutLog WorkoutLog { get; set; }
        public int ExcerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
    }
}

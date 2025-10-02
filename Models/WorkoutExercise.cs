namespace PapisPowerPracticeApi.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        
        public int WorkoutLogId { get; set; }
        public WorkoutLog workoutLog { get; set; }
        public int ExcerciseId { get; set; }
        public Exercise exercise { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
    }
}

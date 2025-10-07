namespace PapisPowerPracticeApi.DTOs.WorkoutExercise.Response
{
    public interface CreateWorkoutExerciseDTO
    {
        public int WorkoutLogId { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
    }
}

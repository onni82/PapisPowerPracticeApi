namespace PapisPowerPracticeApi.DTOs.WorkoutExercise.Request
{
    public class WorkoutExerciseDTO
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
    }
}

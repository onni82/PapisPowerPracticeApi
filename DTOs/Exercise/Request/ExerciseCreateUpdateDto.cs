namespace PapisPowerPracticeApi.DTOs.Exercise.Request
{
    public class ExerciseCreateUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? VideoUrl { get; set; }

        // Lista med MuscleGroup Ids som användaren skickar in
        public List<int> MuscleGroupIds { get; set; } = new();
    }
}

namespace PapisPowerPracticeApi.DTOs.Exercise.Response
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? VideoUrl { get; set; }

        public List<string> MuscleGroups { get; set; } = new();
    }
}

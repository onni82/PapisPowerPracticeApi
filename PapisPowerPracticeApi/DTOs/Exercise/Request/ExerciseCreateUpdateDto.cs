using System.ComponentModel.DataAnnotations;

namespace PapisPowerPracticeApi.DTOs.Exercise.Request
{
    public class ExerciseCreateUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string? VideoUrl { get; set; }

        // Lista med MuscleGroup Ids som användaren skickar in
        public List<int> MuscleGroupIds { get; set; } = new();
    }
}

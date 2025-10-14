using System.ComponentModel.DataAnnotations;

namespace PapisPowerPracticeApi.DTOs
{
    public class MuscleGroupDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}

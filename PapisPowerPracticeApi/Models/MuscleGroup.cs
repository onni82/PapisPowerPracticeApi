namespace PapisPowerPracticeApi.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }


        // Flera-till-flera
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}

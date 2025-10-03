namespace PapisPowerPracticeApi.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public string VideoUrl { get; set; }

        // Flera-till-flera
        public ICollection<MuscleGroup> MuscleGroups { get; set; } = new List<MuscleGroup>();
    }
}

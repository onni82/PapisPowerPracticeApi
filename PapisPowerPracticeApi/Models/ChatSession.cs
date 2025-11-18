namespace PapisPowerPracticeApi.Models
{
    public class ChatSession
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<ChatMsg> Messages { get; set; } = new List<ChatMsg>();
    }
}

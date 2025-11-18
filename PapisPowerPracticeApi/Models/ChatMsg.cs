namespace PapisPowerPracticeApi.Models
{
    public class ChatMsg
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsUserMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid ChatSessionId { get; set; }
        public ChatSession? ChatSession { get; set; }
    }
}

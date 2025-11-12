namespace PapisPowerPracticeApi.DTOs.Chat.Response
{
    public class ChatMessageDTO
    {
        public int Id { get; set; }
        public Guid ChatSessionId { get; set; }
        public string Message { get; set; } = null!;
        public bool IsUserMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

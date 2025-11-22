namespace PapisPowerPracticeApi.Models
{
    public class ChatMsg
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsUserMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

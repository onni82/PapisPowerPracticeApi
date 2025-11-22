namespace PapisPowerPracticeApi.DTOs.Chat.Response
{
    public class ChatMsgDTO
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsUserMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

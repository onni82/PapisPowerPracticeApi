namespace PapisPowerPracticeApi.DTOs.Chat.Response
{
    public class ChatSessionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

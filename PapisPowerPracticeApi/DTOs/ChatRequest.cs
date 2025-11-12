namespace PapisPowerPracticeApi.DTOs
{
    public class ChatRequest
    {
        public Guid? SessionId { get; set; }
        public string Message { get; set; } = null!;
    }
}

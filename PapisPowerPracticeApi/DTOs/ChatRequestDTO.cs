namespace PapisPowerPracticeApi.DTOs
{
    public class ChatRequestDTO
    {
        public Guid? SessionId { get; set; }
        public string Message { get; set; } = null!;
    }
}

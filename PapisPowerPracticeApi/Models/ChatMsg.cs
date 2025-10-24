using Microsoft.AspNetCore.Identity;

namespace PapisPowerPracticeApi.Models
{
    public class ChatMsg
    {
        public int Id { get; set; }

        // Identity kommenteras ut för att undvika problem med migrations och databasen
        /*public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; } = null!;*/

        public string Role { get; set; } = string.Empty; // "user" eller "assistant"
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace PapisPowerPracticeApi.Models
{
    public class WorkoutLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }


        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; } = null!;

    }
}

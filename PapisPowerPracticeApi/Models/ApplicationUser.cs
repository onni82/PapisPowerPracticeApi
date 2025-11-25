using Microsoft.AspNetCore.Identity;

namespace PapisPowerPracticeApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }

        public ICollection<WorkoutLog> WorkoutLogs { get; set; }
    }
}

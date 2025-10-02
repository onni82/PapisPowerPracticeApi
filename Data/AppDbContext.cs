using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExcercise> WorkoutExcercises { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }

    }
}

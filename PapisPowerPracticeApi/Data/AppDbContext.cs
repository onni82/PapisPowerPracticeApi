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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Flera-till-flera-relation Exercise med MuscleGroup
            builder.Entity<Exercise>()
                .HasMany(e => e.MuscleGroups)
                .WithMany(m => m.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseMuscleGroups"));

            builder.Entity<ChatSession>().HasKey(s => s.Id);
            builder.Entity<ChatMessage>().HasKey(m => m.Id);
        }
    }
}

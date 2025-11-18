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
        public DbSet<ChatMsg> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Flera-till-flera-relation Exercise med MuscleGroup
            builder.Entity<Exercise>()
                .HasMany(e => e.MuscleGroups)
                .WithMany(m => m.Exercises)
                .UsingEntity(j => j.ToTable("ExerciseMuscleGroups"));

            builder.Entity<ChatSession>(b => {
                b.HasKey(s => s.Id);
                b.Property(s => s.Title).IsRequired();
                b.Property(s => s.UserId).IsRequired();
                b.Property(s => s.CreatedAt).IsRequired();
            });

            builder.Entity<ChatMsg>(b =>
            {
                b.HasKey(m => m.Id);
                b.Property(m => m.Message).IsRequired();
                b.Property(m => m.UserId).IsRequired();
                b.Property(m => m.Timestamp).IsRequired();
                b.HasOne(m => m.ChatSession)
                 .WithMany(s => s.Messages)
                 .HasForeignKey(m => m.ChatSessionId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

			base.OnModelCreating(builder);
		}
    }
}

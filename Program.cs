
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Repositories;
using PapisPowerPracticeApi.Repositories.Interfaces;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services;
using PapisPowerPracticeApi.Services.IServices;

namespace PapisPowerPracticeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnect"));
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
            builder.Services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

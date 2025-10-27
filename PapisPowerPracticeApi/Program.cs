
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Infrastructure;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories;
using PapisPowerPracticeApi.Repositories.Interfaces;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Services;
using PapisPowerPracticeApi.Services.CalorieIntake;
using PapisPowerPracticeApi.Services.IServices;
using System.Text;
using System.Threading.Tasks;

namespace PapisPowerPracticeApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                };
            });
            builder.Services.AddControllers();
            builder.Services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
            builder.Services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
            builder.Services.AddScoped<IMuscleGroupService, MuscleGroupService>();
            builder.Services.AddScoped<ICalorieCalculatorService, CalorieCalculatorService>();
            builder.Services.AddScoped<IWorkoutLogRepository, WorkoutLogRepository>();
            builder.Services.AddScoped<IWorkoutLogService, WorkoutLogService>();

            // OpenAI-klient
            builder.Services.AddHttpClient("OpenAI", client =>
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:ApiKey"]}");
            });

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
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await DbInitializer.SeedRolesAsync(roleManager);
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

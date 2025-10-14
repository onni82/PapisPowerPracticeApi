using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PapisPowerPracticeApi.Data;
using PapisPowerPracticeApi.Models;
using PapisPowerPracticeApi.Repositories;
using PapisPowerPracticeApi.Repositories.IRepositories;
using PapisPowerPracticeApi.Repositories.Interfaces;
using PapisPowerPracticeApi.Services;
using PapisPowerPracticeApi.Services.CalorieIntake;
using PapisPowerPracticeApi.Services.IServices;
using System.Text;

namespace PapisPowerPracticeApi
{
    public class StartupProduction
    {
        public StartupProduction(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AddDbContext – but simplified for swagger generation
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnect")));

            // Identity (only for type resolution, not used during swagger build)
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // JWT setup
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"] ?? "placeholderkey"))
                };
            });

            // Dependency Injection
            services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
            services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
            services.AddScoped<IMuscleGroupService, MuscleGroupService>();
            services.AddScoped<ICalorieCalculatorService, CalorieCalculatorService>();

            // Swagger + Controllers
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

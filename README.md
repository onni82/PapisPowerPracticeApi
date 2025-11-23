# Papi's Power Practice API

Short overview
- ASP.NET Core Web API targeting .NET 8.
- Uses Entity Framework Core with SQL Server (LocalDB by default).
- ASP.NET Core Identity + JWT authentication.
- Integrates with OpenAI (HTTP client configured) and includes an Azure OpenAI package reference.
- Swagger is enabled in Development for API exploration.

Key technologies
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SqlServer)
- ASP.NET Core Identity + JWT Bearer
- OpenAI / Azure.AI.OpenAI (HTTP client)
- Swashbuckle (Swagger)

Prerequisites
- .NET 8 SDK installed.
- SQL Server LocalDB (or other SQL Server instance) for local development, or update connection string.
- Visual Studio 2022 (with .NET 8 support) or VS Code / CLI.
- Project has a User Secrets ID — recommended to use user secrets or environment variables for secrets.

Quick start (local)
1. Clone
   - git clone https://github.com/onni82/PapisPowerPracticeApi
   - cd PapisPowerPracticeApi

2. Configure secrets
   - Recommended keys (use user-secrets or environment variables):
     - `Jwt:Issuer`
     - `Jwt:Audience`
     - `Jwt:SecretKey` (a strong symmetric key)
     - `OpenAI:ApiKey`
   - Example using dotnet user-secrets (run from the project folder):
     - `dotnet user-secrets set "Jwt:Issuer" "your-issuer"`
     - `dotnet user-secrets set "Jwt:Audience" "your-audience"`
     - `dotnet user-secrets set "Jwt:SecretKey" "a-very-long-secret-key"`
     - `dotnet user-secrets set "OpenAI:ApiKey" "sk-..."`
   - In Visual Studio: right-click the project -> __Manage User Secrets__ to open the secrets editor.

3. Update connection string (if required)
   - Default: `appsettings.json` uses LocalDB:
     - `Data Source=(localdb)\MSSQLLocalDB;Database=PapisPowerPractice;Integrated Security=True;...`
   - To use another SQL Server, update `ConnectionStrings:DefaultConnection` in `appsettings.json` or override via environment variables.

4. Apply EF Core migrations and create database
   - From project directory:
     - `dotnet ef database update`
   - Or in Visual Studio using the __Package Manager Console__:
     - `Update-Database`
   - On startup the app calls `DbInitializer.SeedRolesAsync(...)` to seed Identity roles.

5. Build & run
   - CLI: `dotnet build` then `dotnet run`
   - Visual Studio: open the solution and __Build Solution__, then run (F5 or __Debug > Start Debugging__).
   - When running in Development, Swagger UI will be available at `/swagger`.

What the app wires up (from `Program.cs`)
- `AppDbContext` registered with SQL Server.
- ASP.NET Identity configured with `ApplicationUser` and role management.
- JWT Bearer authentication requires configuration keys mentioned above.
- Scoped services and repositories:
  - `IWorkoutExerciseRepository` / `WorkoutExerciseRepository`
  - `IWorkoutExerciseService` / `WorkoutExerciseService`
  - `IExerciseRepository` / `ExerciseRepository`
  - `IExerciseService` / `ExerciseService`
  - `IMuscleGroupRepository` / `MuscleGroupRepository`
  - `IMuscleGroupService` / `MuscleGroupService`
  - `IChatBotService` / `ChatBotService` (OpenAI)
  - `ICalorieCalculatorService` / `CalorieCalculatorService`
  - `IWorkoutLogRepository` / `WorkoutLogRepository`
  - `IWorkoutLogService` / `WorkoutLogService`
- HTTP client named `OpenAI` is preconfigured to include an `Authorization` header using `OpenAI:ApiKey`.

API exploration
- Swagger: available at `/swagger` in Development environment.
- Controllers are mapped via `app.MapControllers()` — inspect the `Controllers` folder to see available endpoints.

Notes & recommendations
- Keep secrets out of source control — use user secrets for local dev and secure stores (Azure Key Vault, environment variables) for deployments.
- Ensure `Jwt:SecretKey` is long/strong enough for production.
- The project targets .NET 8; ensure your IDE supports .NET 8. Visual Studio 2022 may require the latest updates or the preview channel to fully support .NET 8.
- If you add or change EF model types, create a new migration (`dotnet ef migrations add <Name>`) and apply it.

Missing or next steps (suggested)
- Add a CONTRIBUTING.md and CODE_OF_CONDUCT if this repo will accept external contributions.
- Add automated tests and CI pipeline (GitHub Actions suggested).
- Document controller-specific usage (example requests/responses) in this README or a dedicated API docs folder.

License
- No license file found. Add a `LICENSE` file if you intend to make the project open source.

Created file
- This README (`README.md`) was added to summarize repository structure, local setup, configuration keys, and run instructions.
using Microsoft.AspNetCore.Identity;
using PapisPowerPracticeApi.Models;

namespace PapisPowerPracticeApi.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "User", "Admin" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            // NOTE: Change these credentials before publishing to production.
            var seededUsers = new[]
            {
                new { Email = "user@example.com", Password = "User123!", Role = "User" },
                new { Email = "admin@example.com", Password = "Admin123!", Role = "Admin" }
            };

            foreach (var s in seededUsers)
            {
                var existing = await userManager.FindByEmailAsync(s.Email);
                if (existing != null) continue;

                var user = new ApplicationUser
                {
                    UserName = s.Email,
                    Email = s.Email,
                    EmailConfirmed = true
                    // set any other required ApplicationUser properties here
                };

                var createResult = await userManager.CreateAsync(user, s.Password);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, s.Role);
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentalManagementSystem.Domain.Constants;

namespace RentalmanagementSystem.Infrastructure.Seeder
{
    public class RoleSeeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<RoleSeeder>>();

            var roles = new[]
            {
            Roles.Admin,
            Roles.Customer
        };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    continue;
                }

                var result = await roleManager.CreateAsync(
                    new IdentityRole<Guid>(role)
                );

                if (result.Succeeded)
                {
                    logger.LogInformation(
                        "Role {Role} created successfully",
                        role
                    );
                }
                else
                {
                    var errors = result.Errors
                        .Select(e => e.Description);

                    logger.LogError(
                        "Failed to create role {Role}. Errors: {Errors}",
                        role,
                        string.Join(", ", errors)
                    );
                }
            }
        }
    }
}

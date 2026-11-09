using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentalManagementSystem.Domain.Constants;
using RentalManagementSystem.Domain.Entities;

namespace RentalmanagementSystem.Infrastructure.Seeder
{
    public class AdminSeeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

            var logger = scope.ServiceProvider
                .GetRequiredService<ILogger<AdminSeeder>>();
            try
            {
                var admin = await userManager
                    .FindByEmailAsync("admin@gmail.com");

                if (admin == null)
                {

                    admin = new ApplicationUser
                    {
                        FullName = "Admin",
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        PhoneNumber = "7654321890",
                        DrivingLicenseNumber = "AFS23423D",
                        LicenseExpiryDate = new DateTime(2029, 12,12),
                        Address = "asdfasdf",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(
                        admin,
                        "Admin@123"
                    );

                    if (!result.Succeeded)
                    {
                        var errors = result.Errors
                            .Select(e => e.Description);

                        logger.LogError(
                            "Failed to create admin. Errors: {Errors}",
                            string.Join(", ", errors)
                        );

                        return;
                    }

                    logger.LogInformation("Admin user created");
                }

                if (!await userManager.IsInRoleAsync(admin, Roles.Admin))
                {
                    var result = await userManager.AddToRoleAsync(
                        admin,
                        Roles.Admin
                    );

                    if (!result.Succeeded)
                    {
                        var errors = result.Errors
                            .Select(e => e.Description);

                        logger.LogError(
                            "Failed to assign Admin role. Errors: {Errors}",
                            string.Join(", ", errors)
                        );

                        return;
                    }

                    logger.LogInformation(
                        "Admin role assigned successfully"
                    );
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(
                    ex,
                    "Error occurred while seeding admin"
                );
            }
        }
    }
}

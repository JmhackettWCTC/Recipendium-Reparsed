using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipendium_Reparsed.Models;

namespace Recipendium_Reparsed.Data;

/// <summary>
/// Seeds the Admin role at startup and assigns it to the user whose email matches the configuration value.
/// </summary>
public static class RoleSeeder
{
    /// <summary>
    /// Creates the Admin role if it doesn't exist and assigns it to the configured admin email.
    /// </summary>
    public static async Task SeedAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Create the Admin role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // 2. Read the admin email from configuration
        var adminEmail = configuration["Admin:Email"];
        if (string.IsNullOrEmpty(adminEmail))
        {
            return;
        }

        // 3. Find the user by email
        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            return;
        }

        // 4. Add the user to the Admin role if not already in it
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

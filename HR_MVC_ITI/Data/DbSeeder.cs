using HR_MVC_ITI.Models.Enitityes;
using Microsoft.AspNetCore.Identity;

namespace HR_MVC_ITI.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Create Roles
        string[] roleNames = { "HR", "Employee" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create Default HR Admin User
        var adminEmail = "admin@hr.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Administrator",
                EmailConfirmed = true,
                Role = "HR"
            };

            // You can change the default password here
            var createPowerUser = await userManager.CreateAsync(newAdmin, "Admin@123");
            if (createPowerUser.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "HR");
            }
        }
    }
}

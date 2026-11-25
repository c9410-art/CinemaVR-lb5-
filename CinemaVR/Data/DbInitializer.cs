using Microsoft.AspNetCore.Identity;

namespace CinemaVR.Api.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRoles(
            RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(
                    new IdentityRole("User"));
            }

            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(
                    new IdentityRole("Administrator"));
            }
        }
    }
}
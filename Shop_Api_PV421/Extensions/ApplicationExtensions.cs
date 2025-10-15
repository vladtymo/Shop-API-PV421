using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Shop_Api_PV421.Helpers;

namespace Shop_Api_PV421
{
    public static class ApplicationExtensions
    {
        public static void SeedRolesAndInitialAdmin(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                IdentityInitializer.SeedRolesAsync(roleManager).Wait();
                IdentityInitializer.SeedAdminAsync(userManager).Wait();
            }
        }
    }
}

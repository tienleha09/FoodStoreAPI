using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FoodStoreAPI.Models
{
    public static class IdentitySeedData
    {
        private const string userId = "Admin";
        private const string password = "test4321";

        public static async void EnsurePopulated(IApplicationBuilder builder)
        {
            IdentityContext context = builder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<IdentityContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate(); 
            }
            string[] roles = { "Admin", "Customer", "Contributor" };
            RoleManager<IdentityRole> roleManager = builder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in roles)
            {
                if(!context.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
           
            UserManager<IdentityUser> userManager = builder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            
            IdentityUser adminUser= await userManager.FindByIdAsync(userId);
            if(adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = userId,
                    Email = "admin@example.com",
                    PhoneNumber = "555-1234"
                };
                await userManager.CreateAsync(adminUser, password);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}

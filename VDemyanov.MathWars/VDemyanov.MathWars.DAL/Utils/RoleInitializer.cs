using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDemyanov.MathWars.DAL.Utils
{
    public class RoleInitializer
    {

        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration appConfiguration)
        {
            string adminEmail = appConfiguration["RoleInitializer:Email"];
            string password = appConfiguration["RoleInitializer:Password"];

            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new IdentityRole("admin"));
            if (await roleManager.FindByNameAsync("user") == null)
                await roleManager.CreateAsync(new IdentityRole("user"));
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}

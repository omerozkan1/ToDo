using Microsoft.AspNetCore.Identity;
using OmerOzkan.ToDo.Entities.Domains;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Identity
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });

                var memberRole = await roleManager.FindByNameAsync("Member");

                if (memberRole == null)
                {
                    await roleManager.CreateAsync(new AppRole { Name = "Member" });
                }

                var adminUser = await userManager.FindByNameAsync("omerozkan");

                if (adminUser == null)
                {
                    AppUser user = new AppUser
                    {
                        Name = "Ömer",
                        SurName = "Özkan",
                        UserName = "omerozkan",
                        Email = "omeerozkan52@gmail.com"
                    };
                    await userManager.CreateAsync(user, "1");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}

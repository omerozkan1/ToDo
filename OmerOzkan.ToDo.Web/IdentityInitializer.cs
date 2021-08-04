using Microsoft.AspNetCore.Identity;
using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(ToDoContext context)
        {        
            AppUser user = new AppUser
            {
                Name = "demouser",
                SurName = "surname",
                Email = "demouser@gmail.com",
                PhoneNumber = "1234567890",
                UserName = "demouser",
                NormalizedEmail = "DEMOUSER@GMAIL.COM",
                NormalizedUserName = "DEMOUSER",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            AppRole role = new AppRole
            {
                Name = "Member",
                NormalizedName = "MEMBER"
            };

            if (!context.Roles.Any(x => x.Name == "Member"))
            {
                await context.Roles.AddAsync(role);
            }

            if (!context.Users.Any(x => x.Name == "demouser"))
            {
                PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, "Pass@word1");
                var userEntity = context.Users.AddAsync(user);
                var roleEntity = context.Roles.Any(x => x.Name == "Admin") ? new ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AppRole>>() : context.Roles.AddAsync(new AppRole { Name = "Admin", NormalizedName = "ADMIN" });
                await context.SaveChangesAsync();
                await context.UserRoles.AddAsync(new IdentityUserRole<int> { UserId = userEntity.Result.Entity.Id, RoleId = roleEntity.Result.Entity.Id });
                await context.SaveChangesAsync();
            }
        }
    }
}

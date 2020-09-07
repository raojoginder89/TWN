using HRA.Common.Enums;
using HRA.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using System;

namespace HRA.DAL
{
    public static class SeedData
    {
        public static void SeedAdminUser(UserManager<User> userManager)
        {
            SeedUser("raojoginder89@gmail.com", RoleType.SuperAdmin, userManager);
            SeedUser("joginder.yadav@live.com", RoleType.Admin, userManager);
        }

        private static void SeedUser(string email, RoleType role, UserManager<User> userManager)
        {
            try
            {
                var user = new User
                {
                    FirstName = role.ToString(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    Dob = new DateTime(1990, 2, 3),
                    Ssn = role.ToString(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                if (userManager.FindByNameAsync(user.UserName).Result == null)
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(user, "Admin@123");
                    user.PasswordHash = hashed;

                    var result = userManager.CreateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(user, role.ToString()).Result;
                    }
                }
            }
            finally
            {
                //DO nothing
            }
        }
    }
}

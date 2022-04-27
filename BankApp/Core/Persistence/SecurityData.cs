using BankApp.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Core.Persistence
{
    public class SecurityData
    {
        public static async Task InsertUser(BankDbContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    Name = "Admin",
                    Role = Roles.ADMIN,
                    USDBalance = 200,
                    UserName = "bankAdmin"                    
                };

                await userManager.CreateAsync(user, "Password!1");
            }
        }
    }
}

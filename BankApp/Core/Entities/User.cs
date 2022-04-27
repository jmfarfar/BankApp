using Microsoft.AspNetCore.Identity;

namespace BankApp.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public Roles Role { get; set; }
        public decimal USDBalance { get; set; }
    }

}

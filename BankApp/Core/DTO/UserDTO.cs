using BankApp.Entities;

namespace BankApp.Core.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public Roles Role { get; set; }
        public decimal USDBalance { get; set; }
        public string Token { get; set; }
    }
}

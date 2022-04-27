using BankApp.Core.DTO;
using BankApp.Entities;
using MediatR;

namespace BankApp.Core.Register
{
    public class UserRegisterCommand : IRequest<UserDTO>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public decimal USDBalance { get; set; }
    }
}

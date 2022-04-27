using BankApp.Core.DTO;
using MediatR;

namespace BankApp.Core.Login
{
    public class UserLoginCommand : IRequest<UserDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}

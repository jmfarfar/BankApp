using BankApp.Core.DTO;
using MediatR;

namespace BankApp.Core.LoggedUser
{
    public class LoggedUserCommand : IRequest<UserDTO>
    {
    }
}

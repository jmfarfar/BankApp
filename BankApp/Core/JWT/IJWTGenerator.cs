using BankApp.Entities;

namespace BankApp.Core.JWT
{
    public interface IJWTGenerator
    {
        string CreateToken(User user);
    }
}

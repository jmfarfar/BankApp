using FluentValidation;

namespace BankApp.Core.Login
{
    public class UserLoginValidation : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidation()
        {
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.UserName).NotEmpty();
        }
    }
}

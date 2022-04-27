using FluentValidation;

namespace BankApp.Core.Register
{
    public class UserRegisterValidation : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterValidation()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            //RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.USDBalance).NotEmpty();
        }
    }
}

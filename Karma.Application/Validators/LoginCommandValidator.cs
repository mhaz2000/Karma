using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage("نام کاربری الزامی است.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("رمز عبور الزامی است.");
        }
    }
}

using FluentValidation;
using Karma.Application.Commands;
using System.Text.RegularExpressions;

namespace Karma.Application.Validators
{
    internal class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
    {
        public SetPasswordCommandValidator()
        {
            RuleFor(c => c.Password).NotEmpty().WithMessage("رمز عبور الزامی است.");
            RuleFor(c=>c.Password).Must(c=> Regex.IsMatch(c, "^\\S{8,}$")).When(c=> !string.IsNullOrEmpty(c.Password))
                .WithMessage("رمز عبور می‌بایست حداقل 8 کاراکتر و بدون فاصله باشد.");
        }
    }
}

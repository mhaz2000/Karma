using FluentValidation;
using Karma.Application.Commands;
using System.Text.RegularExpressions;

namespace Karma.Application.Validators
{
    internal class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(c => c.OldPassword).NotEmpty().When(c=>c.OldPassword is not null).WithMessage("رمز عبور قبلی الزامی است.");
            RuleFor(c => c.NewPassword).NotEmpty().WithMessage("رمز عبور الزامی است.");
            RuleFor(c=>c.NewPassword).Must(c=> Regex.IsMatch(c, "^\\S{8,}$")).When(c=> !string.IsNullOrEmpty(c.NewPassword))
                .WithMessage("رمز عبور می‌بایست حداقل 8 کاراکتر و بدون فاصله باشد.");
        }
    }
}

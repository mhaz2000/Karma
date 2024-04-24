using FluentValidation;
using Karma.Application.Commands;
using System.Text.RegularExpressions;

namespace Karma.Application.Validators
{
    internal class OtpRequestCommandValidator : AbstractValidator<OtpRequestCommand>
    {
        public OtpRequestCommandValidator()
        {
            RuleFor(c => c.Phone).Must(c => Regex.IsMatch(c, @"^(\+98|0)?9\d{9}$")).WithMessage("فرمت شماره موبایل صحیح نمی‌باشد.");
        }
    }
}

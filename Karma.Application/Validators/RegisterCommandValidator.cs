using FluentValidation;
using Karma.Application.Commands;
using System.Text.RegularExpressions;

namespace Karma.Application.Validators
{
    internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Phone).Must(c => Regex.IsMatch(c, @"^(\+98|0)?9\d{9}$")).WithMessage("فرمت شماره موبایل صحیح نمی‌باشد.");
        }
    }
    
    internal class ResumeFilterCommandValidator : AbstractValidator<ResumeFilterCommand>
    {
        public ResumeFilterCommandValidator()
        {
            RuleFor(c=>c.YoungerThan).LessThanOrEqualTo(DateTime.Now).When(c=> c.YoungerThan is not null)
                .WithMessage("مقدار وارد شده برای فیلتر سن صحیح نمی‌باشد.");

            RuleFor(c => c.OlderThan).LessThanOrEqualTo(DateTime.Now).When(c => c.OlderThan is not null)
                .WithMessage("مقدار وارد شده برای فیلتر سن صحیح نمی‌باشد.");

            RuleFor(c => c.OlderThan).GreaterThan(c=> c.YoungerThan).When(c => c.YoungerThan is not null && c.OlderThan is not null)
                .WithMessage("مقدار وارد شده برای فیلتر سن صحیح نمی‌باشد.");
        }
    }
}

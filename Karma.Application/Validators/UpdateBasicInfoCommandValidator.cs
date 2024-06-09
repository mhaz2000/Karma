using FluentValidation;
using Karma.Application.Commands;
using System.Text.RegularExpressions;

namespace Karma.Application.Validators
{
    internal class UpdateBasicInfoCommandValidator : AbstractValidator<UpdateBasicInfoCommand>
    {
        public UpdateBasicInfoCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("نام الزامی است.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("نام خانوادگی الزامی است.");
            RuleFor(c => c.City).NotEmpty().WithMessage("شهر الزامی است.");
            RuleFor(c => c.BirthDate).LessThan(DateTime.Now).WithMessage("تاریخ تولد صحیح نیست.");
            RuleFor(c => c.Email).Must(c=> Regex.IsMatch(c,
                "^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"))
                .When(c=> c.Email is not null)
                .WithMessage("تاریخ تولد صحیح نیست.");
        }
    }
}

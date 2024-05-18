using FluentValidation;
using Karma.Application.Commands;

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
        }
    }
}

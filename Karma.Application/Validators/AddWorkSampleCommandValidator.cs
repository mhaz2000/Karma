using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class AddWorkSampleCommandValidator : AbstractValidator<AddWorkSampleCommand>
    {
        public AddWorkSampleCommandValidator()
        {
            RuleFor(c => c.Link).NotEmpty().WithMessage("لینک نمونه کار الزامی است.");
        }
    }
}

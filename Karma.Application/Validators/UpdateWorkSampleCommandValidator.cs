using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class UpdateWorkSampleCommandValidator : AbstractValidator<UpdateWorkSampleCommand>
    {
        public UpdateWorkSampleCommandValidator()
        {
            RuleFor(c => c.Link).NotEmpty().WithMessage("لینک نمونه کار الزامی است.");
        }
    }
}

using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class AddAdditionalSkillCommandValidator : AbstractValidator<AddAdditionalSkillCommand>
    {
        public AddAdditionalSkillCommandValidator() {
            RuleFor(c=>c.Title).NotEmpty().WithMessage("عنوان مهارت تکمیلی الزامی است.");
        }
    }
}

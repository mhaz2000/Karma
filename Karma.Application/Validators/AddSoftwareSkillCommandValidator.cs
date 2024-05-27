using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class AddSoftwareSkillCommandValidator : AbstractValidator<AddSoftwareSkillCommand>
    {
        public AddSoftwareSkillCommandValidator() { }
    }
}

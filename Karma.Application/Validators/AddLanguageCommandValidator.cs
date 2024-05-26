using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class AddLanguageCommandValidator : AbstractValidator<AddLanguageCommand>
    {
        public AddLanguageCommandValidator() { }
    }
}

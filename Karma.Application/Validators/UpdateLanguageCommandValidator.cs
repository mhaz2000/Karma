using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator() { }
    }
}

using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class UpdateLanguageCommand : IBaseCommand
    {
        public int LanguageId { get; set; }
        public LanguageLevel Level { get; set; }
        public void Validate() => new UpdateLanguageCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}

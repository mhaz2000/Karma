using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class AddAdditionalSkillCommand : IBaseCommand
    {
        public required string Title { get; set; }
        public void Validate() => new AddAdditionalSkillCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}

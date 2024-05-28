using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class SetPasswordCommand : IBaseCommand {
        public required string Password { get; set; }

        public void Validate() => new SetPasswordCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}

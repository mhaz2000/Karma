using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class UpdatePasswordCommand : IBaseCommand
    {
        public string? OldPassword { get; set; }
        public required string NewPassword { get; set; }

        public void Validate() => new UpdatePasswordCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}

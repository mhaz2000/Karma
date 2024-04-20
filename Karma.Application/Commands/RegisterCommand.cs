using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class RegisterCommand : IBaseCommand
    {
        public required string Phone { get; set; }

        public void Validate() => new RegisterCommandValidator().Validate(this).RaiseExceptionIfRequired(); 
    }
}

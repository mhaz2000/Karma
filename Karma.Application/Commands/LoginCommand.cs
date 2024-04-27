using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class LoginCommand : IBaseCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public void Validate() => new LoginCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}

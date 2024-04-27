using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class PhoneConfirmationCommand : IBaseCommand
    {
        public string Phone { get; set; }
        public string OtpCode { get; set; }
        public void Validate() => new PhoneConfirmationCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}

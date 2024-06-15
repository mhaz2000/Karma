using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;

namespace Karma.Application.Commands
{
    public class AddWorkSampleCommand : IBaseCommand
    {
        public required string Link { get; set; }
        public void Validate() => new AddWorkSampleCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}

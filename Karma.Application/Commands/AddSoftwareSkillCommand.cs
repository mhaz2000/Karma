using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class AddSoftwareSkillCommand : IBaseCommand
    {
        public int SoftwareSkillId { get; set; }
        public SoftwareSkillLevel Level { get; set; }
        public void Validate() => new AddSoftwareSkillCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}

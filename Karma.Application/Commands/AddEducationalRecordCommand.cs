using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class AddEducationalRecordCommand : IBaseCommand
    {
        public DegreeLevel DegreeLevel { get; set; }
        public string? DiplomaMajor { get; set; }
        public int? MajorId { get; set; }
        public int? UniversityId { get; set; }
        public float? GPA { get; set; }
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public bool StillEducating { get; set; }
        public void Validate() => new AddEducationalRecordCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}

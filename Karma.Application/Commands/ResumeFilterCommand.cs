using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class ResumeFilterCommand : IBaseCommand
    {
        public string? Code { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? YoungerThan { get; set; }
        public DateTime? OlderThan { get; set; }
        public string? City { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public IEnumerable<MilitaryServiceStatus> MilitaryServiceStatuses { get; set; } = Enumerable.Empty<MilitaryServiceStatus>();
        public IEnumerable<DegreeLevel> DegreeLevels { get; set; } = Enumerable.Empty<DegreeLevel>();
        public IEnumerable<int> LanguageIds { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<int> SoftwareSkillIds { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<int> JobCategoryIds { get; set; } = Enumerable.Empty<int>();
        public CareerExperienceLength? CareerExperienceLength { get; set; }

        public void Validate() => new ResumeFilterCommandValidator().Validate(this).RaiseExceptionIfRequired();
    }
}

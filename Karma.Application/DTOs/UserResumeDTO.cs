using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record UserResumeDTO : IMapFrom<Resume>
    {
        public required string Code { get; set; }
        public required string MainJobTitle { get; set; }
        public string? Description { get; set; }
        public Guid? ResumeFileId { get; set; }
        public IEnumerable<SocialMediaDTO>? SocialMedias { get; set; }
        public IList<EducationalRecordDTO>? EducationalRecords { get; set; }
        public IList<CareerRecordDTO>? CareerRecords { get; set; }
        public IList<LanguageDTO>? Languages { get; set; }
        public IList<SoftwareSkillDTO>? SoftwareSkills { get; set; }
        public IList<AdditionalSkillDTO>? AdditionalSkills { get; set; }
        public IList<WorkSampleDTO>? WorkSamples { get; set; }
    }
}

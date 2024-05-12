using Karma.Core.Entities.Base;

namespace Karma.Core.Entities
{
    public class Resume : IBaseEntity
    {
        public Resume()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual required User User { get; set; }
        public virtual IEnumerable<EducationalRecord> EducationalRecords { get; set; } = Enumerable.Empty<EducationalRecord>();
        public virtual IEnumerable<CareerRecord> CareerRecords { get; set; } = Enumerable.Empty<CareerRecord>();
        public virtual IEnumerable<Language> Languages { get; set; } = Enumerable.Empty<Language>();
        public virtual IEnumerable<SoftwareSkill> SoftwareSkills { get; set; } = Enumerable.Empty<SoftwareSkill>();
        public virtual IEnumerable<AdditionalSkill> AdditionalSkills { get; set; } = Enumerable.Empty<AdditionalSkill>();
    }
}

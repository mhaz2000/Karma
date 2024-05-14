using Karma.Core.Entities.Base;

namespace Karma.Core.Entities
{
    public class Resume : IBaseEntity
    {
        public Resume()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;

            SocialMedias = new HashSet<SocialMedia>();
            EducationalRecords = new HashSet<EducationalRecord>();
            CareerRecords = new HashSet<CareerRecord>();
            Languages = new HashSet<Language>();
            SoftwareSkills = new HashSet<SoftwareSkill>();
            AdditionalSkills = new HashSet<AdditionalSkill>();
        }

        public Resume(User user) : this()
        {
            User = user;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string MainJobTitle { get; set; } = string.Empty;
        public string? Description { get; set; }

        public virtual required User User { get; set; } 
        public virtual IEnumerable<SocialMedia> SocialMedias { get; set; }
        public virtual IEnumerable<EducationalRecord> EducationalRecords { get; set; }
        public virtual IEnumerable<CareerRecord> CareerRecords { get; set; }
        public virtual IEnumerable<Language> Languages { get; set; }
        public virtual IEnumerable<SoftwareSkill> SoftwareSkills { get; set; }
        public virtual IEnumerable<AdditionalSkill> AdditionalSkills { get; set; }
    }
}

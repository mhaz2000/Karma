using Karma.Core.Entities.Base;
using System.Globalization;

namespace Karma.Core.Entities
{
    public class Resume : IBaseEntity
    {
        public Resume()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;

            SocialMedias = new HashSet<SocialMedia>();
            EducationalRecords = new List<EducationalRecord>();
            CareerRecords = new List<CareerRecord>();
            Languages = new List<Language>();
            SoftwareSkills = new List<SoftwareSkill>();
            AdditionalSkills = new List<AdditionalSkill>();
        }

        public Resume(User user) : this()
        {
            User = user;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public required string Code { get; set; }
        public string MainJobTitle { get; set; } = string.Empty;
        public string? Description { get; set; }

        public virtual required User User { get; set; } 
        public virtual IEnumerable<SocialMedia> SocialMedias { get; set; }
        public virtual IList<EducationalRecord> EducationalRecords { get; set; }
        public virtual IList<CareerRecord> CareerRecords { get; set; }
        public virtual IList<Language> Languages { get; set; }
        public virtual IList<SoftwareSkill> SoftwareSkills { get; set; }
        public virtual IList<AdditionalSkill> AdditionalSkills { get; set; }
        public Guid? ResumeFileId { get; set; }

        internal void Validate()
        {
            PersianCalendar pc = new PersianCalendar();
            if (User.BirthDate is not null && EducationalRecords.Any(c => c.FromYear <= pc.GetYear(User.BirthDate!.Value)))
                throw new ApplicationException("سال ورود به دانشگاه باید از سال تولد شما بزرگتر باشد.");

            var sortedList = EducationalRecords.OrderBy(c => c.FromYear).ThenBy(c => c.ToYear).ToList();

            for (int i =1; i < sortedList.Count(); i++)
                if (sortedList[i].FromYear < sortedList[i-1].ToYear)
                    throw new ApplicationException("سال های تدریس دانشگاه نمی‌تواند با هم تداخل زمانی داشته باشد.");
        }
    }
}

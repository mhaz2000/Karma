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
        public virtual IList<EducationalRecord> EducationalRecords { get; set; }
        public virtual IEnumerable<CareerRecord> CareerRecords { get; set; }
        public virtual IEnumerable<Language> Languages { get; set; }
        public virtual IEnumerable<SoftwareSkill> SoftwareSkills { get; set; }
        public virtual IEnumerable<AdditionalSkill> AdditionalSkills { get; set; }

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

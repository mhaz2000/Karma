using Karma.Core.Entities.Base;
using Karma.Core.Enums;

namespace Karma.Core.Entities
{
    public class SoftwareSkill : IBaseEntity
    {
        public SoftwareSkill()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual required SystemSoftwareSkill SystemSoftwareSkill { get; set; }
        public SoftwareSkillLevel SoftwareSkillLevel { get; set; }
    }
}

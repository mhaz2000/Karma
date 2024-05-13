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

        public required string Title { get; set; }
        public SofwareSkillLevel SofwareSkillLevel { get; set; }
    }
}

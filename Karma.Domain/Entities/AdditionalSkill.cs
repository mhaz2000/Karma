using Karma.Core.Entities.Base;

namespace Karma.Core.Entities
{
    public class AdditionalSkill : IBaseEntity
    {
        public AdditionalSkill()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public AdditionalSkill(string title)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Title = title;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public required string Title { get; set; }
    }
}

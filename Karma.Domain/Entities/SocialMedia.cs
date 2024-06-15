using Karma.Core.Entities.Base;
using Karma.Core.Enums;

namespace Karma.Core.Entities
{
    public class SocialMedia : IBaseEntity
    {
        public SocialMedia()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set;}

        public required SocialMediaType SocialMediaType { get; set; }
        public required string Link { get; set; }
    }
}

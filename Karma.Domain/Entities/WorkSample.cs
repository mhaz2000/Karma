using Karma.Core.Entities.Base;

namespace Karma.Core.Entities
{
    public class WorkSample : IBaseEntity
    {
        public WorkSample()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public required string Link { get; set; }
    }
}

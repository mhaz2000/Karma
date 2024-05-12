using Karma.Core.Entities.Base;
using Karma.Core.Enums;

namespace Karma.Core.Entities
{
    public class CareerRecord : IBaseEntity
    {
        public CareerRecord()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public DateTime CreatedAt {get; set;}

        public required string JobTitle { get; set; }
        public virtual required JobCategory JobCategory { get; set; }
        public SeniorityLevel SeniorityLevel { get; set; }
        public required string CompanyName { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int ToMonth { get; set; }
        public int ToYear { get; set; }
        public bool CurrentJob { get; set; } = false;
    }
}

using Karma.Core.Entities.Base;
using Karma.Core.Enums;

namespace Karma.Core.Entities
{
    public class EducationalRecord : IBaseEntity
    {
        public EducationalRecord()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DegreeLevel DegreeLevel { get; set; }
        public virtual required Major Major { get; set; }
        public virtual required University University { get; set; }
        public float? GPA { get; set; }
        public int FromYear { get; set; }
        public int? ToYear { get; set; }
        public bool StillEducating { get; set; }

    }
}

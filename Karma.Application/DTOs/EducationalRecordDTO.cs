using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;

namespace Karma.Application.DTOs
{
    public record EducationalRecordDTO : IMapFrom<EducationalRecord>
    {
        public Guid Id { get; set; }
        public DegreeLevel DegreeLevel { get; set; }
        public virtual required MajorDTO Major { get; set; }
        public virtual required UniversityDTO University { get; set; }
        public float? GPA { get; set; }
        public int FromYear { get; set; }
        public int? ToYear { get; set; }
        public bool StillEducating { get; set; }
    }
}

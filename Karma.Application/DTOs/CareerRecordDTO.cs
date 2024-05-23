using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;

namespace Karma.Application.DTOs
{
    public record CareerRecordDTO : IMapFrom<CareerRecord>
    {
        public Guid Id { get; set; }
        public required string JobTitle { get; set; }
        public virtual required JobCategoryDTO JobCategory { get; set; }
        public SeniorityLevel SeniorityLevel { get; set; }
        public required string CompanyName { get; set; }
        public virtual required CountryDTO Country { get; set; }
        public virtual required CityDTO? City { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }
        public bool CurrentJob { get; set; }

    }
}

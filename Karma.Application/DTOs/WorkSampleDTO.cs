using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record WorkSampleDTO : IMapFrom<WorkSample>
    {
        public Guid Id { get; set; }
        public required string Link { get; set; }
    }
}

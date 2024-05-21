using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record JobCategoryDTO : IMapFrom<JobCategory>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

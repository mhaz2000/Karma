using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record SystemSoftwareSkillDTO : IMapFrom<SystemSoftwareSkill>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

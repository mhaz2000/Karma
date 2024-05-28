using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record AdditionalSkillDTO : IMapFrom<AdditionalSkill>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}

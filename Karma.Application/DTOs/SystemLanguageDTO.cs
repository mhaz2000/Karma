using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record SystemLanguageDTO : IMapFrom<SystemLanguage>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

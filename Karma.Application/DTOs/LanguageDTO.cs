using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;

namespace Karma.Application.DTOs
{
    public record LanguageDTO : IMapFrom<Language>
    {
        public Guid Id { get; set; }
        public required SystemLanguageDTO Language { get; set; }
        public LanguageLevel Level { get; set; }
    }
}

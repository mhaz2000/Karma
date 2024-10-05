using Karma.Application.Helpers;
using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;
using System.Text.Json.Serialization;

namespace Karma.Application.DTOs
{
    public record LanguageDTO : IMapFrom<Language>
    {
        public Guid Id { get; set; }
        [JsonPropertyName("Language")]
        public required SystemLanguageDTO SystemLanguage { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
        public string LanguageLevelTitle => LanguageLevel.GetDescription();
        public string LanguageTitle => SystemLanguage.Title;

    }
}

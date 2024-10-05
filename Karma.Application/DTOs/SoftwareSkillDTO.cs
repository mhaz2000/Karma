using Karma.Application.Helpers;
using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;
using System.Text.Json.Serialization;

namespace Karma.Application.DTOs
{
    public record SoftwareSkillDTO : IMapFrom<SoftwareSkill>
    {
        public Guid Id { get; set; }
        [JsonPropertyName("SoftwareSkill")]
        public required SystemSoftwareSkillDTO SystemSoftwareSkill { get; set; }
        public SoftwareSkillLevel SoftwareSkillLevel { get; set; }
        public string SoftwareSkillLevelTitle => SoftwareSkillLevel.GetDescription();
        public string SoftwareSkillTitle => SystemSoftwareSkill.Title;
    }
}

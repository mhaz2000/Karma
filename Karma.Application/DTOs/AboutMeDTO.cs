﻿using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;
using System.Text.Json.Serialization;

namespace Karma.Application.DTOs
{
    public record AboutMeDTO
    {
        public Guid? ImageId { get; set; }
        public required string MainJobTitle { get; set; }
        public string? Description { get; set; }

        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; } = Enumerable.Empty<SocialMediaDTO>();
    }

    public record SocialMediaDTO : IMapFrom<SocialMedia>
    {
        [JsonPropertyName("type")]
        public SocialMediaType SocialMediaType { get; set; }
        public required string Link { get; set; }
    }
}

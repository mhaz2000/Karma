using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record CountryDTO : IMapFrom<Country>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

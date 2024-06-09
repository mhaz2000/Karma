using Karma.Application.Mappings;
using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record BasicInfoDTO : IMapFrom<User>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public required string MaritalStatus { get; set; }
        public required string MilitaryServiceStatus { get; set; }
        public required string Gender { get; set; }
    }
}

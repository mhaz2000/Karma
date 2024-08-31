using Karma.Application.Mappings;
using Karma.Core.Enums;
using Karma.Core.ViewModels;

namespace Karma.Application.DTOs
{
    public record ResumeQueryDTO : IMapFrom<ExpandedResume>
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string JobTitle { get; set; }
        public string? City { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public MilitaryServiceStatus? MilitaryServiceStatus { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Telephone { get; set; }
        public DegreeLevel? DegreeLevel { get; set; }
    }
}

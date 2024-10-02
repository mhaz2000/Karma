using Karma.Core.Entities;

namespace Karma.Application.DTOs
{
    public record ResumePrintDTO
    {
        public byte[]? LogoFile { get; set; }
        public required string PhoneNumber { get; set; }
        public required string FirstName { get; set; }
        public string? Email { get; set; }
        public required string LastName { get; set; }
        public required string MainJobTitle { get; set; }
        public string? Description { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Gender { get; set; }
        public required string MilitaryServiceStatus { get; set; }
        public string? City { get; set; }
        public DateTime? BirthDate { get; set; }
        public required string Code { get; set; }
        public Guid? Logo { get; set; }
        public IEnumerable<SocialMediaDTO>? SocialMedias { get; set; }
        public IEnumerable<EducationalRecordDTO>? EducationalRecords { get; set; }
        public IEnumerable<CareerRecordDTO>? CareerRecords { get; set; }
        public IEnumerable<LanguageDTO>? Languages { get; set; }
        public IEnumerable<SoftwareSkillDTO>? SoftwareSkills { get; set; }
        public IEnumerable<AdditionalSkillDTO>? AdditionalSkills { get; set; }
        public IEnumerable<WorkSampleDTO>? WorkSamples { get; set; }
    }
}

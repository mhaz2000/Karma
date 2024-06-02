using Karma.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karma.Core.ViewModels
{
    public class ExpandedResume
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Code { get; set; }
        public string? MainJobTitle {get; set;}
        public string? Description {get; set;}
        public DateTime? BirthDate {get; set;}
        public string? City {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public Gender? Gender {get; set;}
        public MaritalStatus? MaritalStatus {get; set;}
        public MilitaryServiceStatus? MilitaryServiceStatus { get; set;}
        public string? PhoneNumber {get; set;}
        public string? Telephone {get; set;}
        public DegreeLevel? DegreeLevel {get; set;}
        public string? DiplomaMajor {get; set;}
        public double GPA {get; set;}
        public int? UniversityFromYear {get; set;}
        public int? UniversityToYear {get; set;}
        public bool StillEducating {get; set;}
        public int? MajorId {get; set;}
        public string? MajorTitle {get; set;}
        public int? UniversityId {get; set;}
        public string? UniversityTitle {get; set;}
        public string? JobTitle {get; set;}
        public SeniorityLevel? SeniorityLevel {get; set;}
        public string? CompanyName {get; set;}
        public int? CareerRecordFromMonth {get; set;}
        public int? CareerRecordFromYear {get; set;}
        public int? CareerRecordToMonth {get; set;}
        public int? CareerRecordToYear {get; set;}
        public string? CurrentJob {get; set;}
        public int? JobCategoryId { get; set;}
        public string? JobCategory { get; set;}
        public LanguageLevel? LanguageLevel {get; set;}
        public int? LanguageId {get; set;}
        public string? Language {get; set;}
        public SoftwareSkillLevel? SoftwareSkillLevel {get; set;}
        public int? SoftwareSkillId {get; set;}
        public string? SoftwareSkill {get; set;}
        public string? AdditionalSkill {get; set;}

    }
}

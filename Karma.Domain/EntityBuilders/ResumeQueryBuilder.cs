using Karma.Core.Entities;
using Karma.Core.Enums;
using Karma.Core.ViewModels;
using System.Globalization;

namespace Karma.Core.EntityBuilders
{
    public class ResumeQueryBuilder
    {
        private IEnumerable<ExpandedResume> _resumes;

        public ResumeQueryBuilder(IEnumerable<ExpandedResume> resumes)
        {
            _resumes = resumes;
        }

        public ResumeQueryBuilder WithCode(string? code)
        {
            _resumes = string.IsNullOrEmpty(code) ? _resumes : _resumes.Where(c => c.Code == code);
            return this;
        }

        public ResumeQueryBuilder WithYoungerThan(DateTime? youngerThan)
        {
            _resumes = youngerThan is null ? _resumes : _resumes.Where(c => c.BirthDate is not null && c.BirthDate <= youngerThan);
            return this;
        }

        public ResumeQueryBuilder WithOlderThan(DateTime? olderThan)
        {
            _resumes = olderThan is null ? _resumes : _resumes.Where(c => c.BirthDate is not null && c.BirthDate >= olderThan);
            return this;
        }

        public ResumeQueryBuilder WithCity(string? city)
        {
            _resumes = string.IsNullOrEmpty(city) ? _resumes : _resumes.Where(c => !string.IsNullOrEmpty(c.City) && c.City.Contains(city));
            return this;
        }

        public ResumeQueryBuilder WithGender(Gender? gender)
        {
            _resumes = gender is null ? _resumes : _resumes.Where(c => c.Gender is not null && c.Gender == gender);
            return this;
        }

        public ResumeQueryBuilder WithMaritalStatus(MaritalStatus? maritalStatus)
        {
            _resumes = maritalStatus is null ? _resumes : _resumes.Where(c => c.MaritalStatus is not null && c.MaritalStatus == maritalStatus);
            return this;
        }

        public ResumeQueryBuilder WithMilitaryServiceStatuses(IEnumerable<MilitaryServiceStatus> militaryServiceStatuses)
        {
            _resumes = !militaryServiceStatuses.Any() ? _resumes : _resumes.Where(c => c.MilitaryServiceStatus is not null && militaryServiceStatuses.Contains(c.MilitaryServiceStatus.Value));
            return this;
        }

        public ResumeQueryBuilder WithDegreeLevels(IEnumerable<DegreeLevel> degreeLevels)
        {
            _resumes = !degreeLevels.Any() ? _resumes : _resumes.Where(c => c.DegreeLevel is not null && degreeLevels.Contains(c.DegreeLevel.Value));
            return this;
        }

        public ResumeQueryBuilder WithLanguages(IEnumerable<int> languegeIds)
        {
            _resumes = !languegeIds.Any() ? _resumes : _resumes.Where(c => c.LanguageId is not null && languegeIds.Contains(c.LanguageId.Value));
            return this;
        }

        public ResumeQueryBuilder WithSoftwareSkill(IEnumerable<int> softwareSkillIds)
        {
            _resumes = !softwareSkillIds.Any() ? _resumes : _resumes.Where(c => c.SoftwareSkillId is not null && softwareSkillIds.Contains(c.SoftwareSkillId.Value));
            return this;
        }

        public ResumeQueryBuilder WithJobCategories(IEnumerable<int> jobCategoryIds)
        {
            _resumes = !jobCategoryIds.Any() ? _resumes : _resumes.Where(c => c.JobCategoryId is not null && jobCategoryIds.Contains(c.JobCategoryId.Value));
            return this;
        }

        public ResumeQueryBuilder WithCareerExperienceLength(CareerExperienceLength? careerExperienceLength)
        {
            if (careerExperienceLength is null)
                return this;

            PersianCalendar pc = new PersianCalendar();
            _resumes.Where(c => c.CareerRecordFromYear is not null).GroupBy(c => c.UserId).Where(c =>
            {
                var workingDays = c.Where(t => t.CareerRecordToYear is not null)
                    .Sum(t => (new DateTime(t.CareerRecordToYear!.Value, t.CareerRecordToMonth!.Value, 1, pc)
                    - new DateTime(t.CareerRecordFromYear!.Value, t.CareerRecordFromMonth!.Value, 1, pc)).TotalDays);

                workingDays += c.Where(t => t.StillEducating).Sum(t => (DateTime.Now - new DateTime(t.CareerRecordFromYear!.Value, t.CareerRecordFromMonth!.Value, 1, pc)).TotalDays);

                return (careerExperienceLength == CareerExperienceLength.WithoutExperience && workingDays == 0) ||
                       (careerExperienceLength == CareerExperienceLength.LessThanOneYear && workingDays < 365) ||
                       (careerExperienceLength == CareerExperienceLength.BetweenOneAndThreeYears && workingDays >= 365 && workingDays < 3 * 365) ||
                       (careerExperienceLength == CareerExperienceLength.BetweenFiveAndTenYears && workingDays >= 3 * 365 && workingDays <= 10 * 365) ||
                       (careerExperienceLength == CareerExperienceLength.MoreThanTenYears && workingDays >= 10 * 365);
            });

            return this;
        }

        public IEnumerable<ExpandedResume> Build()
        {
            return _resumes;
        }
    }
}

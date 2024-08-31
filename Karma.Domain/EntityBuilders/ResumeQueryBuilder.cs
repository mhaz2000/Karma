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

        public ResumeQueryBuilder WithJobTitle(string? jobTitle)
        {
            _resumes = string.IsNullOrEmpty(jobTitle) ? _resumes : _resumes.Where(c => c.JobTitle?.Contains(jobTitle) ?? false);
            return this;
        }

        public ResumeQueryBuilder WithYoungerThan(DateTime? youngerThan)
        {
            _resumes = youngerThan is null ? _resumes : _resumes.Where(c => c.BirthDate is not null && c.BirthDate >= youngerThan);
            return this;
        }

        public ResumeQueryBuilder WithOlderThan(DateTime? olderThan)
        {
            _resumes = olderThan is null ? _resumes : _resumes.Where(c => c.BirthDate is not null && c.BirthDate <= olderThan);
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
            var usersId = _resumes.Where(c => c.CareerRecordFromYear is not null).Select(s => new
            {
                s.UserId,
                s.CareerRecordId,
                s.CareerRecordFromMonth,
                s.CareerRecordFromYear,
                s.CareerRecordToMonth,
                s.CareerRecordToYear,
                s.CurrentJob
            }).Distinct().AsEnumerable().Where(c =>
                {
                    double workingDays = 0;
                    if (c.CareerRecordToYear is not null)
                        workingDays = (new DateTime(c.CareerRecordToYear!.Value, c.CareerRecordToMonth!.Value, 1, pc)
                            - new DateTime(c.CareerRecordFromYear!.Value, c.CareerRecordFromMonth!.Value, 1, pc)).TotalDays;

                    if (c.CurrentJob is not null && c.CurrentJob.Value)
                        workingDays += (DateTime.Now - new DateTime(c.CareerRecordFromYear!.Value, c.CareerRecordFromMonth!.Value, 1, pc)).TotalDays;

                    return (careerExperienceLength == CareerExperienceLength.WithoutExperience && workingDays == 0) ||
                           (careerExperienceLength == CareerExperienceLength.LessThanOneYear && workingDays < 365) ||
                           (careerExperienceLength == CareerExperienceLength.BetweenOneAndThreeYears && workingDays >= 365 && workingDays < 3 * 365) ||
                           (careerExperienceLength == CareerExperienceLength.BetweenThreeAndFiveYears && workingDays >= 3* 365 && workingDays < 5 * 365) ||
                           (careerExperienceLength == CareerExperienceLength.BetweenFiveAndTenYears && workingDays >= 5 * 365 && workingDays <= 10 * 365) ||
                           (careerExperienceLength == CareerExperienceLength.MoreThanTenYears && workingDays >= 10 * 365);
                }).Select(c => c.UserId);

            _resumes = _resumes.Where(c => usersId.Contains(c.UserId));

            return this;
        }

        public IEnumerable<ExpandedResume> Build()
        {
            return _resumes;
        }
    }
}

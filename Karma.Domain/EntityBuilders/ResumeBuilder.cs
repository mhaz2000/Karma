using Karma.Core.Entities;

namespace Karma.Core.EntityBuilders
{
    public class ResumeBuilder
    {
        private Resume _resume;

        public ResumeBuilder(Resume? resume, User user)
        {
            _resume = resume ?? new Resume() { User = user };
        }

        public ResumeBuilder WithMainJobTitle(string mainJobTitle)
        {
            _resume.MainJobTitle = mainJobTitle;
            return this;
        }

        public ResumeBuilder WithDescription(string? description)
        {
            _resume.Description = description;
            return this;
        }

        public ResumeBuilder WithUser(User user)
        {
            _resume.User = user;
            return this;
        }

        public ResumeBuilder WithSocialMedias(IEnumerable<SocialMedia> socialMedias)
        {
            _resume.SocialMedias = socialMedias.ToList();
            return this;
        }

        public ResumeBuilder WithEducationalRecords(IEnumerable<EducationalRecord> educationalRecords)
        {
            _resume.EducationalRecords = educationalRecords.ToList();
            return this;
        }

        public ResumeBuilder WithCareerRecords(IEnumerable<CareerRecord> careerRecords)
        {
            _resume.CareerRecords = careerRecords.ToList();
            return this;
        }

        public ResumeBuilder WithLanguages(IEnumerable<Language> languages)
        {
            _resume.Languages = languages.ToList();
            return this;
        }

        public ResumeBuilder WithSoftwareSkills(IEnumerable<SoftwareSkill> softwareSkills)
        {
            _resume.SoftwareSkills = softwareSkills.ToList();
            return this;
        }

        public ResumeBuilder WithAdditionalSkills(IEnumerable<AdditionalSkill> additionalSkills)
        {
            _resume.AdditionalSkills = additionalSkills.ToList();
            return this;
        }

        public Resume Build()
        {
            return _resume;
        }
    }
}

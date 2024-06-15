namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository FileRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICityRepository CityRepository { get; }
        IUserRepository UserRepository { get; }
        IMajorRepository MajorRepository { get; }
        IResumeRepository ResumeRepository { get; }
        ICountryRepository CountryRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IJobCategoryRepository JobCategoryRepository { get; }
        ISocialMediaRepository SocialMediaRepository { get; }
        ICareerRecordRepository CareerRecordRepository { get; }
        ISoftwareSkillRepository SoftwareSkillRepository { get; }
        ISystemLanguageRepository SystemLanguageRepository { get; }
        IAdditionalSkillRepository AdditionalSkillRepository { get; }
        IEducationalRecordRepository EducationalRecordRepository { get; }
        ISystemSoftwareSkillRepository SystemSoftwareSkillRepository { get; }

        IExpandedResumeViewRepository ExpandedResumeViewRepository { get; }

        Task<int> CommitAsync();
    }
}

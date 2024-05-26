namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository RoleRepository { get; }
        ICityRepository CityRepository { get; }
        IMajorRepository MajorRepository { get; }
        IUserRepository UserRepository { get; }
        IResumeRepository ResumeRepository { get; }
        ICountryRepository CountryRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IJobCategoryRepository JobCategoryRepository { get; }
        ISocialMediaRepository SocialMediaRepository { get; }
        ICareerRecordRepository CareerRecordRepository { get; }
        ISystemLanguageRepository SystemLanguageRepository { get; }
        IEducationalRecordRepository EducationalRecordRepository { get; }
        ISystemSoftwareSkillRepository SystemSoftwareSkillRepository { get; }

        Task<int> CommitAsync();
    }
}

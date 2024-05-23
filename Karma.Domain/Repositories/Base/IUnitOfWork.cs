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
        ICareerRecordRepository CareerRecordRepository { get; }
        IJobCategoryRepository JobCategoryRepository { get; }
        IEducationalRecordRepository EducationalRecordRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        ISocialMediaRepository SocialMediaRepository { get; }

        Task<int> CommitAsync();
    }
}

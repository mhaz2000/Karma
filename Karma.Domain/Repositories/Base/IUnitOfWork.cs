namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository RoleRepository { get; }
        IMajorRepository MajorRepository { get; }
        IUserRepository UserRepository { get; }
        IResumeRepository ResumeRepository { get; }
        IEducationalRepository EducationalRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        ISocialMediaRepository SocialMediaRepository { get; }

        Task<int> CommitAsync();
    }
}

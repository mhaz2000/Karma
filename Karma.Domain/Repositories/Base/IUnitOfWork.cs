namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository RoleRepository { get; }
        IMajorRepository MajorRepository { get; }
        IUserRepository UserRepository { get; }
        IResumeRepository ResumeRepository { get; }
        ISocialMediaRepository SocialMediaRepository { get; }

        Task<int> CommitAsync();
    }
}

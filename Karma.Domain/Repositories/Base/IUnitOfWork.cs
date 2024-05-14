namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IResumeRepository ResumeRepository { get; }
        ISocialMediaRepository SocialMediaRepository { get; }

        Task<int> CommitAsync();
    }
}

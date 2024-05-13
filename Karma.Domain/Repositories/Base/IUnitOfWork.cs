namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IResumeRepository ResumeRepository { get; }

        Task<int> CommitAsync();
    }
}

namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        Task<int> CommitAsync();
    }
}

namespace Karma.Core.Repositories.Base
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task<int> CommitAsync();
    }
}

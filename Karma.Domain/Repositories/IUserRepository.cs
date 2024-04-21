using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task CreateUserAsync(string phone);
    }
}

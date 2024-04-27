using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckUserPasswordAsync(User user, string password);
        Task CreateUserAsync(string phone);
    }
}

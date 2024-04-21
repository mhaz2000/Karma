using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task CreateUserAsync(string phone)
        {
            var entity = new User() { PhoneNumber = phone};

            await _dbSet.AddAsync(entity);
        }
    }
}

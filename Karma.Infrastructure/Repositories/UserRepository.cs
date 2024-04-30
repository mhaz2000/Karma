using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Karma.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private const string UserRole = "user";

        public UserRepository(DataContext dataContext, UserManager<User> userManager) : base(dataContext)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckUserPasswordAsync(User user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task CreateUserAsync(string phone)
        {
            var entity = new User() { PhoneNumber = phone};

            var idolUser = await _dbSet.FirstOrDefaultAsync(c => c.PhoneNumber == phone && !c.PhoneNumberConfirmed);

            if (idolUser is not null)
                _dbSet.Remove(idolUser);

            await _userManager.AddToRoleAsync(entity, UserRole);
            await _dbSet.AddAsync(entity);
        }
    }
}

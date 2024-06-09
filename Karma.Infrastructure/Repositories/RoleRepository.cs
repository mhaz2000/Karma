using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Karma.Infrastructure.Repositories
{
    public class RoleRepository : Repository<IdentityRole<Guid>>, IRoleRepository
    {
        public RoleRepository(DataContext dataContext) : base(dataContext)
        {
            
        }

        public async Task<bool> CheckIfUserIsAdminAsync(User user)
        {
            var rolesId = Context.UserRoles.Where(c => c.UserId == user.Id).Select(s=>s.RoleId);
            return await Task.FromResult(Context.Roles.Where(c => rolesId.Contains(c.Id)).Any(c => c.Name == "admin"));
        }

        public async Task<IList<IdentityRole<Guid>>> GetUserRolesAsync(User user)
        {
            var roles = Context.UserRoles.Where(c => c.UserId == user.Id).Select(s => s.RoleId);
            return await Context.Roles.Where(c => roles.Contains(c.Id)).ToListAsync();
        }
    }
}

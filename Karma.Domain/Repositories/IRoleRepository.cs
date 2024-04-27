using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using Microsoft.AspNetCore.Identity;

namespace Karma.Core.Repositories
{
    public interface IRoleRepository : IRepository<IdentityRole<Guid>>
    {
        Task<IList<IdentityRole<Guid>>> GetUserRolesAsync(User user);

    }
}

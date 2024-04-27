using Karma.Application.Base;
using Karma.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Karma.Application.Helpers.TokenHelper
{
    public interface ITokenGenerator
    {
        JsonWebTokenModel TokenGeneration(User user, JwtIssuerOptionsModel jwtOptions, IList<IdentityRole<Guid>> userRoles);
    }
}

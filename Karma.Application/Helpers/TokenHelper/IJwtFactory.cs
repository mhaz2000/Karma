using Karma.Core.Entities;
using System.Security.Claims;

namespace Karma.Application.Helpers.TokenHelper
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(User user, IList<string> userRoles, IEnumerable<string> roleIds, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}

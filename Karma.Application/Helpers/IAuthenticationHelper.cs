using Karma.Application.DTOs;
using Karma.Core.Entities;

namespace Karma.Application.Helpers
{
    public interface IAuthenticationHelper
    {
        Task<AuthenticatedUserDTO> GetToken(User user);
    }
}

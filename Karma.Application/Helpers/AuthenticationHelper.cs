using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Helpers.TokenHelper;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly JwtIssuerOptionsModel _jwtIssuerOptionsModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationHelper(JwtIssuerOptionsModel jwtIssuerOptionsModel, IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
        {
            _jwtIssuerOptionsModel = jwtIssuerOptionsModel;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticatedUserDTO> GetToken(User user)
        {
            var userRoles = await _unitOfWork.RoleRepository.GetUserRolesAsync(user);
            var token = _tokenGenerator.TokenGeneration(user, _jwtIssuerOptionsModel, userRoles);

            return new AuthenticatedUserDTO()
            {
                RefreshToken = token.RefreshToken,
                AuthToken = token.AuthToken,
                IsAdmin = await _unitOfWork.RoleRepository.CheckIfUserIsAdminAsync(user)
            };
        }
    }
}

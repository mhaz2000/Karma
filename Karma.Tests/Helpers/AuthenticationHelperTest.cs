using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Helpers;
using Karma.Application.Helpers.TokenHelper;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using Microsoft.AspNetCore.Identity;

namespace Karma.Tests.Helpers
{
    public class AuthenticationHelperTest
    {
        private readonly AuthenticationHelper _authenticationHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthenticationHelperTest()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _tokenGenerator = A.Fake<ITokenGenerator>();

            _authenticationHelper = new AuthenticationHelper(new JwtIssuerOptionsModel(), _unitOfWork, _tokenGenerator);
        }

        [Fact]
        public async Task Must_Get_User_Token()
        {
            //Assert
            var user = new User();
            var expectedToken = new AuthenticatedUserDTO() { AuthToken = "Dummy Token", RefreshToken = "Dummy Refersh Token" };
            var jwt = new JsonWebTokenModel() { AuthToken = expectedToken.AuthToken, ExpiresIn = 100, RefreshToken =  expectedToken.RefreshToken };
            IList<IdentityRole<Guid>> rolesId = new List<IdentityRole<Guid>>();

            A.CallTo(() => _unitOfWork.RoleRepository.GetUserRolesAsync(user))
                .Returns(rolesId);

            A.CallTo(() => _tokenGenerator.TokenGeneration(user, A<JwtIssuerOptionsModel>._, rolesId))
                .Returns(jwt);

            //Act
            var token = await _authenticationHelper.GetToken(user);

            //Assert
            token.Should().Be(expectedToken);
        }
    }
}

using FakeItEasy;
using Karma.Application.Helpers;
using Karma.Application.Notifications;
using Karma.Application.Services;
using Karma.Core.Caching;
using Karma.Core.Repositories.Base;
using Microsoft.Extensions.Configuration;

namespace Karma.Tests.Services.Users
{
    public class UserServiceTest
    {
        protected readonly UserService _userService;
        protected readonly KavenegarFactory _kavenegarFactory;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ICacheProvider _cacheProvider;
        protected readonly IConfiguration _configuration;
        protected readonly IAuthenticationHelper _authenticationHelper;
        public UserServiceTest()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _cacheProvider = A.Fake<ICacheProvider>();
            _configuration = A.Fake<IConfiguration>();
            _authenticationHelper = A.Fake<IAuthenticationHelper>();
            _kavenegarFactory = A.Fake<KavenegarFactory>();

            _configuration["InMemoryCaching:OptExpirationTime"] = "120";

            _userService = new UserService(_unitOfWork, _configuration, _cacheProvider, _authenticationHelper, _kavenegarFactory);
        }
    }
}

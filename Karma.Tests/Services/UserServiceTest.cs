using FakeItEasy;
using Karma.Application.Services;
using Karma.Core.Repositories.Base;

namespace Karma.Tests.Services
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        public UserServiceTest()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _userService = new UserService(_unitOfWork);
        }
    }
}

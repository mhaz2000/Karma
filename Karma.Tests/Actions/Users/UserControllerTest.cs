using FakeItEasy;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;

namespace Karma.Tests.Actions.Users
{
    public abstract class UserControllerTest
    {
        protected readonly UsersController _controller;
        protected readonly IUserService _userService;
        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _controller = new UsersController(_userService);

            _controller.ControllerContext = Fixture.FakeControllerContext();
        }
    }
}

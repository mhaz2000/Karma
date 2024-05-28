using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Users
{
    public class PasswordInitializedTests : UserControllerTest
    {
        [Fact]
        public async Task Must_Check_If_Password_Has_Been_Initialized()
        {
            //Act
            var result = await _controller.CheckIfPasswordInitialized();
            var response = (OkObjectResult)result;

            //Assert
            A.CallTo(() => _userService.CheckIfPasswordInitializedAsync(A<Guid>._)).MustHaveHappenedOnceExactly();

            response.StatusCode.Should().Be(200);
        }
    }
}

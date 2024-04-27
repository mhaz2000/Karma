using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Karma.Tests.Actions.Users
{
    public class LoginTest : UserControllerTest
    {
        [Fact]
        public async Task Login_Should_Return_Error_When_Username_Is_Not_Valid()
        {
            //Arragne
            var command = new LoginCommand() { Password = "123", Username = string.Empty };

            // Act
            var act = async () => await _controller.Login(command);

            //Arrange
            await act.Should().ThrowAsync<ValidationException>().WithMessage("نام کاربری الزامی است.");
            A.CallTo(() => _userService.Login(command)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Login_Should_Return_Error_When_Password_Is_Not_Valid()
        {
            //Arragne
            var command = new LoginCommand() { Password = string.Empty, Username = "Username" };

            // Act
            var act = async () => await _controller.Login(command);

            //Arrange
            await act.Should().ThrowAsync<ValidationException>().WithMessage("رمز عبور الزامی است.");
            A.CallTo(() => _userService.Login(command)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Login_Should_Return_Error_When_Password_And_Username_Are_Not_Valid()
        {
            //Arragne
            var command = new LoginCommand() { Password = string.Empty, Username = string.Empty };

            // Act
            var act = async () => await _controller.Login(command);

            //Arrange
            await act.Should().ThrowAsync<ValidationException>().WithMessage("نام کاربری الزامی است.\nرمز عبور الزامی است.");
            A.CallTo(() => _userService.Login(command)).MustNotHaveHappened();
        }

        [Fact]
        public async Task User_Should_Login_When_Credentials_Are_Valid()
        {
            //Arragne
            var command = new LoginCommand() { Password = "faked password", Username = "faked username" };
            var expectedAuthenticatedUserDto = new AuthenticatedUserDTO() { AuthToken = "faked auth token", RefreshToken = "faked refresh token" };

            A.CallTo(() => _userService.Login(command)).Returns(expectedAuthenticatedUserDto);

            // Act
            var response = await _controller.Login(command);

            var result = (OkObjectResult)response;

            A.CallTo(() => _userService.Login(command))
                .MustHaveHappenedOnceExactly();

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value!.GetType().GetProperty("message", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)!
                .GetValue(result.Value, null)
                .Should().Be("با موفقیت وارد شدید.");


            result.Value!.GetType().GetProperty("value", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)!
                .GetValue(result.Value, null)
                .Should().Be(expectedAuthenticatedUserDto);
        }
    }
}

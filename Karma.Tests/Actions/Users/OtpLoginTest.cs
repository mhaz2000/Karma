using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Karma.Tests.Actions.Users
{
    public class OtpLoginTest : UserControllerTest
    {
        [Theory]
        [InlineData("0912495985")]
        [InlineData("091249598555")]
        [InlineData("12457895642")]
        [InlineData("0124568953")]
        [InlineData("+98915426")]
        [InlineData("")]
        [InlineData("+9891245789563")]
        [InlineData("+98912457895")]
        public async Task Otp_Login_Should_Return_Error_When_Phone_Number_Is_Not_Valid(string phoneNumber)
        {
            //Arragne
            var command = new OtpLoginCommand() { Phone = phoneNumber, OtpCode = "11111" };

            // Assert
            await _controller.Invoking(x => x.OtpLogin(command))
                .Should().ThrowAsync<ValidationException>().WithMessage("فرمت شماره موبایل صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Should_Return_Error_When_Otp_Code_Is_Not_Valid()
        {
            //Arragne
            var command = new OtpLoginCommand() { Phone = "09207831300", OtpCode = "" };

            // Assert
            await _controller.Invoking(x => x.OtpLogin(command))
                .Should().ThrowAsync<ValidationException>().WithMessage("کد تایید الزامی است.");
        }

        [Fact]
        public async Task User_Should_Be_loggedIn_Successfully()
        {
            //Arrange
            var command = new OtpLoginCommand() { Phone = "09207831300", OtpCode = "11111" };
            var expectedAuthenticatedUserDto = new AuthenticatedUserDTO() { AuthToken = "Faked token", RefreshToken = "Faked refresh token" };

            A.CallTo(() => _userService.OtpLoginAsync(command)).Returns(expectedAuthenticatedUserDto);

            //Act
            var act = async () => await _controller.OtpLogin(command);

            var response = await _controller.OtpLogin(command);
            var result = (OkObjectResult)response;

            A.CallTo(() => _userService.OtpLoginAsync(command))
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

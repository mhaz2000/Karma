using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Karma.Tests.Actions.Users
{
    public class OtpRequestTest : UserControllerTest
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
        public async Task Otp_Request_Should_Return_Error_When_Phone_Number_Is_Not_Valid(string phoneNumber)
        {
            //Arragne
            var command = new OtpRequestCommand() { Phone = phoneNumber };

            // Assert
            await _controller.Invoking(x => x.OtpRequest(command))
                .Should().ThrowAsync<ValidationException>().WithMessage("فرمت شماره موبایل صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Otp_Code_Must_Be_Sent_When_Phone_Format_Is_Valid()
        {
            //Arrange
            var command = new OtpRequestCommand() { Phone = "09207831300" };

            //Act
            var act = async () => await _controller.OtpRequest(command);

            var response = await _controller.OtpRequest(command);
            var result = (OkObjectResult)response;

            A.CallTo(() => _userService.OtpRequestAsync(command))
                .MustHaveHappenedOnceExactly();

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value!.GetType()
                .GetProperty("message", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)!
                .GetValue(result.Value, null)
                .Should().Be("کد تایید ارسال شد.");
        }
    }
}

using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Karma.Tests.Actions.Users
{
    public class SetPasswordTests : UserControllerTest
    {
        [Fact]
        public async Task Should_Throw_Exception_When_Password_Is_Empty()
        {
            //Arragne
            var command = new SetPasswordCommand() { Password = string.Empty };

            // Act
            var act = async () => await _controller.SetPassword(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("رمز عبور الزامی است.");
            A.CallTo(() => _userService.SetPasswordAsync(command, A<Guid>._)).MustNotHaveHappened();
        }
        
        [Theory]
        [InlineData("1234567")]
        [InlineData("12345545df 67")]
        [InlineData("12 5545df 67")]
        [InlineData("adsfa")]
        public async Task Should_Throw_Exception_When_Password_Is_Not_Valid(string password)
        {
            //Arragne
            var command = new SetPasswordCommand() { Password = password };

            // Act
            var act = async () => await _controller.SetPassword(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("رمز عبور می‌بایست حداقل 8 کاراکتر و بدون فاصله باشد.");
            A.CallTo(() => _userService.SetPasswordAsync(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Set_Password_When_Data_Is_Valid()
        {
            //Arragne
            var command = new SetPasswordCommand() { Password = "FakePassword" };

            // Act
            var response = await _controller.SetPassword(command);
            var result = (OkObjectResult)response;

            A.CallTo(() => _userService.SetPasswordAsync(command, A<Guid>._))
                .MustHaveHappenedOnceExactly();

            //Assert
            result.StatusCode.Should().Be(200);
        }

    }
}

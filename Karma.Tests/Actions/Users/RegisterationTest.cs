﻿using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Karma.Tests.Actions.Users
{
    public class RegisterationTest : UserControllerTest
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
        public async Task Should_Return_Error_When_Inputs_Are_Not_Valid(string phoneNumber)
        {
            //Arragne
            var command = new RegisterCommand() { Phone = phoneNumber };

            // Assert
            await _controller.Invoking(x => x.Register(command))
                .Should().ThrowAsync<ValidationException>().WithMessage("فرمت شماره موبایل صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task User_Should_Be_Registered_Successfully()
        {
            //Arrange
            var command = new RegisterCommand() { Phone = "09207831300" };

            //Act
            var act = async () => await _controller.Register(command);

            var response = await _controller.Register(command);
            var result = (OkObjectResult)response;

            A.CallTo(() => _userService.RegisterAsync(command))
                .MustHaveHappenedOnceExactly();

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value!.GetType()
                .GetProperty("message", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)!
                .GetValue(result.Value, null)
                .Should().Be("ثبت نام با موفقیت انجام شد، کد ارسال شده به تلفن همراه خود را وارد نمایید.");
        }
    }
}

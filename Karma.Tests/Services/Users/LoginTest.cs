using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Users
{
    public class LoginTest : UserServiceTest
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Phone_Number_Is_Not_Valid()
        {
            //Arrange
            var command = new OtpLoginCommand() { OtpCode = "0000", Phone = "0000000000" };
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);

            //Act
            var act = async () => await _userService.OtpLogin(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Get(A<string>._)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربری با این شماره تلفن یافت نشد.");
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Otp_Code_Is_Not_Matched()
        {
            //Arrange
            var command = new OtpLoginCommand() { OtpCode = "0000", Phone = "09109828656" };
            User user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _cacheProvider.Get(command.OtpCode)).Returns(string.Empty);

            //Act
            var act = async () => await _userService.OtpLogin(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Get(A<string>._)).MustHaveHappenedOnceExactly();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کد وارد شده صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Must_Return_Token_When_Phone_And_OtpCode_Are_Matched()
        {
            //Arrange
            var command = new OtpLoginCommand() { OtpCode = "254654", Phone = "09154525668" };
            User user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _cacheProvider.Get(A<string>._)).Returns(command.OtpCode);

            //Act
            var result = await _userService.OtpLogin(command);

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Get(A<string>._)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
        }
    }
}

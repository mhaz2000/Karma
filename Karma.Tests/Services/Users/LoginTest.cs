using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Users
{
    public class LoginTest : UserServiceTest
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Username_Is_Not_Valid()
        {
            //Arrange
            var command = new LoginCommand() { Password = "123", Username = "not vaild username" };
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);

            //Act
            var act = async () => await _userService.Login(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _authenticationHelper.GetToken(A<User>._)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("نام کاربری یا رمز عبور اشتباه است.");
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Password_Is_Not_Valid()
        {
            //Arrange
            var command = new LoginCommand() { Password = "not valid password", Username = "vaild username" };
            User user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(user, command.Password)).Returns(false);

            //Act
            var act = async () => await _userService.Login(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _authenticationHelper.GetToken(A<User>._)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("نام کاربری یا رمز عبور اشتباه است.");
        }

        [Fact]
        public async Task Must_Return_Token_When_Credentials_Are_Valid()
        {
            //Arrange
            var command = new LoginCommand() { Password = "254654", Username = "09154525668" };
            var expectedAuthenticatedUserDto = new AuthenticatedUserDTO() { RefreshToken = "faked refresh token", AuthToken = "faked auth token" };
            User user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(user, command.Password)).Returns(true);
            A.CallTo(() => _authenticationHelper.GetToken(user)).Returns(expectedAuthenticatedUserDto);

            //Act
            var result = await _userService.Login(command);

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _authenticationHelper.GetToken(A<User>._)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().Be(expectedAuthenticatedUserDto);
        }
    }
}

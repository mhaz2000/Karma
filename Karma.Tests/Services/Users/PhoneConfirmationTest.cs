using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Users
{
    public class PhoneConfirmationTest : UserServiceTest
    {

        [Fact]
        public async Task Must_Throw_Exception_When_Phone_Number_Is_Not_Valid()
        {
            //Arrange
            var command = new PhoneConfirmationCommand() { Phone = "0000000000" };
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);

            //Act
            var act = async () => await _userService.PhoneConfirmationAsync(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Get(A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربری با این شماره تلفن یافت نشد.");
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Otp_Code_Is_Not_Matched()
        {
            //Arrange
            var command = new PhoneConfirmationCommand() { OtpCode = "0000", Phone = "09109828656" };
            User user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _cacheProvider.Get(command.OtpCode)).Returns(string.Empty);

            //Act
            var act = async () => await _userService.PhoneConfirmationAsync(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Get(A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کد وارد شده صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Must_Return_Token_When_Phone_And_OtpCode_Are_Matched()
        {
            //Arrange
            var command = new PhoneConfirmationCommand() { OtpCode = "254654", Phone = "09154525668" };
            User user = new User();
            AuthenticatedUserDTO fakedAuthenticatedUserDto = new AuthenticatedUserDTO() { AuthToken = "Faked Auth Token", RefreshToken = "Faked Refresh Token"};

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _cacheProvider.Get(A<string>._)).Returns(command.OtpCode);
            A.CallTo(() => _authenticationHelper.GetToken(user)).Returns(fakedAuthenticatedUserDto);

            //Act
            var result = await _userService.PhoneConfirmationAsync(command);

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Get(A<string>._)).MustHaveHappenedOnceExactly();

            result.Should().NotBeNull();
            result.Should().Be(fakedAuthenticatedUserDto);
        }
    }
}

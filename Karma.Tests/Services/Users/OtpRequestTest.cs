using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Users
{
    public class OtpRequestTest : UserServiceTest
    {

        [Fact]
        public async Task Must_Throw_Exception_When_Phone_Number_Is_Not_Valid()
        {
            //Arrange
            var command = new OtpRequestCommand() { Phone = "0000000000" };
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);

            //Act
            var act = async () => await _userService.OtpRequest(command);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Set(A<string>._, A<string>._, A<int>._)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربری با این شماره تلفن یافت نشد.");
        }

        [Fact]
        public async Task Must_Set_Otp_Code_In_Cache()
        {
            //Arrange
            var command = new OtpRequestCommand() { Phone = "09124568596" };
            User user = new User();
            string CachedUser = null;

            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).Returns(user);
            A.CallTo(() => _cacheProvider.Get(A<string>._)).Returns(CachedUser);

            //Act
            await _userService.OtpRequest(command);

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.FirstOrDefaultAsync(A<Expression<Func<User, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _cacheProvider.Set(A<string>._, A<string>._, A<int>._)).MustHaveHappenedOnceExactly();

        }
    }
}

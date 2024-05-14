using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Users
{
    public class RegistrationTest : UserServiceTest
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Phone_Number_Is_Duplicated()
        {
            //Arragne
            var command = new RegisterCommand() { Phone = "09109828956" };

            A.CallTo(() => _unitOfWork.UserRepository.AnyAsync(A<Expression<Func<User, bool>>>._))
                .Returns(true);

            //Act
            await _userService.Invoking(c => c.RegisterAsync(command)).Should()
                .ThrowAsync<ManagedException>().WithMessage("این شماره موبایل قبلا در سامانه ثبت شده است.");
        }

        [Fact]
        public async Task Must_Create_User_When_Data_Is_Valid()
        {
            //Arrange
            var command = new RegisterCommand() { Phone = "09109828956" };

            A.CallTo(() => _unitOfWork.UserRepository.AnyAsync(A<Expression<Func<User, bool>>>._))
                .Returns(false);

            //Act
            await _userService.Invoking(c => c.RegisterAsync(command)).Should().NotThrowAsync();

            A.CallTo(() => _cacheProvider.Set(command.Phone, A<string>._, A<int>._));

            A.CallTo(() => _unitOfWork.UserRepository.CreateUserAsync(command.Phone))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _unitOfWork.CommitAsync())
                .MustHaveHappenedOnceExactly();
        }
    }
}

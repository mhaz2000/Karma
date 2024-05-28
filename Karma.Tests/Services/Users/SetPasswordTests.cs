using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Users
{
    public class SetPasswordTests : UserServiceTest
    {
        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            var command = new SetPasswordCommand() { Password = "Fake Password" };
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _userService.SetPasswordAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Set_Password()
        {
            var command = new SetPasswordCommand() { Password = "Fake Password" };
            User? user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _userService.SetPasswordAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

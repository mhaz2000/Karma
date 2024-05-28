using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Users
{
    public class UpdatePasswordTests : UserServiceTest
    {
        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdatePasswordCommand() { NewPassword = "Fake New Password"};
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _userService.UpdatePasswordAsync(command ,Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Old_Password_Is_Null_But_It_Is_Required()
        {
            //Arrange
            var command = new UpdatePasswordCommand() { NewPassword = "Fake New Password" };
            User? user = new User() { PasswordHash = "Fake Password Hash"};

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _userService.UpdatePasswordAsync(command, Guid.NewGuid());

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("رمز عبور قبلی الزامی است.");

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Old_Password_Is_Not_Matched()
        {
            //Arrange
            var command = new UpdatePasswordCommand() { NewPassword = "Fake New Password", OldPassword = "Wrong Fake Password" };
            User? user = new User() { PasswordHash = "Fake Password Hash" };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).Returns(false);

            //Act
            var act = async () => await _userService.UpdatePasswordAsync(command, Guid.NewGuid());
            await act.Should().ThrowAsync<ManagedException>().WithMessage("رمز عبور قبلی صحیح نیست.");

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

        }

        [Fact]
        public async Task Should_Updaet_Password_When_It_Has_Not_Been_Initialized()
        {
            //Arrange
            var command = new UpdatePasswordCommand() { NewPassword = "Fake New Password" };
            User? user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _userService.UpdatePasswordAsync(command, Guid.NewGuid());

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task Should_Update_Password_When_It_Has_Been_Initialized()
        {
            //Arrange
            var command = new UpdatePasswordCommand() { NewPassword = "Fake New Password", OldPassword = "Fake Old Password" };
            User? user = new User() { PasswordHash = "Fake Password Hash" };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).Returns(true);

            //Act
            var act = async () => await _userService.UpdatePasswordAsync(command, Guid.NewGuid());

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UserRepository.CheckUserPasswordAsync(A<User>._, A<string>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }
    }
}

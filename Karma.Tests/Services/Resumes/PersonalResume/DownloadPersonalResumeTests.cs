using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.PersonalResume
{
    public class DownloadPersonalResumeTests : ResumeServiceTests
    {
        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var userId = Guid.NewGuid();
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);

            //Act
            var act = async () => await _resumeReadService.DownloadPersonalResumeAsync(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _fileService.GetFileAsync(userId)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Resume_Cannot_Be_Found()
        {
            var userId = Guid.NewGuid();
            User user = new User();
            Resume? resume = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeReadService.DownloadPersonalResumeAsync(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.GetFileAsync(userId)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("رزومه شما یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Personal_Resume_Has_Not_Been_Uploaded()
        {
            var userId = Guid.NewGuid();
            User user = new User();
            Resume? resume = new Resume() { Code = string.Empty, User = user};

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeReadService.DownloadPersonalResumeAsync(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.GetFileAsync(userId)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("رزومه شخصی بارگذاری نشده است.");
        }

        [Fact]
        public async Task Must_Download_User_Personal_Resume()
        {
            var userId = Guid.NewGuid();
            User user = new User();
            Resume? resume = new Resume() { Code = string.Empty, User = user, ResumeFileId = Guid.NewGuid() };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeReadService.DownloadPersonalResumeAsync(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fileService.GetFileAsync(resume.ResumeFileId.Value)).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Resumes
{
    public class GetUserResumeTests : ResumeServiceTests
    {
        [Fact]
        public async Task Should_Throw_Exception_When_Resume_Cannot_Be_Found()
        {
            //Arrange
            Resume resume = null;

            A.CallTo(() => _unitOfWork.ResumeRepository.GetByIdAsync(A<Guid>._)).Returns(resume);

            //Act
            var act = async () => await _resumeReadService.GetUserResumeAsync(Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.ResumeRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mapper.Map<UserResumeDTO>(A<Resume>._)).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("رزومه کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Get_User_Resume()
        {
            //Arrange
            Resume resume = new Resume() { Code = "123456", User = new User() };

            A.CallTo(() => _unitOfWork.ResumeRepository.GetByIdAsync(A<Guid>._)).Returns(resume);

            //Act
            var act = async () => await _resumeReadService.GetUserResumeAsync(Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.ResumeRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mapper.Map<UserResumeDTO>(A<Resume>._)).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

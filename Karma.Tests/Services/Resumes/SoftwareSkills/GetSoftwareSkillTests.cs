using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.SoftwareSkills
{
    public class GetSoftwareSkillTests : ResumeServiceTests
    {
        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var userId = Guid.NewGuid();
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);

            //Act
            var act = async () => await _resumeReadService.GetLanguages(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();

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
            var act = async () => await _resumeReadService.GetLanguages(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("رزومه شما یافت نشد.");
        }

        [Fact]
        public async Task Should_Return_Languages()
        {
            var userId = Guid.NewGuid();
            User user = new User();
            Resume? resume = new Resume() { User = user, Code = string.Empty };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);
            A.CallTo(() => _mapper.Map<IEnumerable<SoftwareSkillDTO>>(A<IQueryable<SoftwareSkillDTO>>._)).Returns(new List<SoftwareSkillDTO>());
            //Act
            var act = async () => await _resumeReadService.GetLanguages(userId);
            var result = await act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync<ManagedException>();
        }
    }
}

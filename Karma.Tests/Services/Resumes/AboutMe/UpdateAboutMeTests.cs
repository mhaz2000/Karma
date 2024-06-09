using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.AboutMe
{
    public class UpdateAboutMeTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAboutMeTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateAboutMeCommand() { MainJobTitle = "Test" };
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeService.UpdateAboutMeAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SocialMediaRepository.AddRangeAsync(A<IEnumerable<SocialMedia>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SocialMediaRepository.RemoveRange(A<IEnumerable<SocialMedia>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Create_Resume_If_It_Deos_Not_Exist()
        {
            //Arrange
            var command = new UpdateAboutMeCommand() { MainJobTitle = "Test" };
            User? user = new User();
            Resume? resume = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeService.UpdateAboutMeAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SocialMediaRepository.AddRangeAsync(A<IEnumerable<SocialMedia>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SocialMediaRepository.RemoveRange(A<IEnumerable<SocialMedia>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync<ManagedException>();
        }

        [Fact]
        public async Task Should_Update_Resume_If_It_Exists()
        {
            //Arrange
            var command = new UpdateAboutMeCommand() { MainJobTitle = "Test" };
            User? user = new User();
            Resume? resume = new Resume() { User = user, Code = string.Empty };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeService.UpdateAboutMeAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SocialMediaRepository.AddRangeAsync(A<IEnumerable<SocialMedia>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SocialMediaRepository.RemoveRange(A<IEnumerable<SocialMedia>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync<ManagedException>();
        }
    }
}

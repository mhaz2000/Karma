using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.SoftwareSkills
{
    public class AddSoftwareSkillTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddSoftwareSkillTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }


        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new AddSoftwareSkillCommand();
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeService.AddSoftwareSkill(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.AddAsync(A<SoftwareSkill>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_System_Software_Skill_Cannot_Be_Found()
        {
            //Arrange
            var command = new AddSoftwareSkillCommand();
            User? user = new User();
            SystemSoftwareSkill? systemSoftwareSkill = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).Returns(systemSoftwareSkill);

            //Act
            var act = async () => await _resumeService.AddSoftwareSkill(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.AddAsync(A<SoftwareSkill>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای مهارت نرم افزاری صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Should_Create_Resume_If_It_Deos_Not_Exist()
        {
            //Arrange
            var command = new AddSoftwareSkillCommand();
            User? user = new User();
            SystemSoftwareSkill? systemSoftwareSkill = new SystemSoftwareSkill() { Title = "Fake Software Skill" };
            Resume? resume = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).Returns(systemSoftwareSkill);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeService.AddSoftwareSkill(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.AddAsync(A<SoftwareSkill>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Should_Create_Resume_If_It_Exists()
        {
            //Arrange
            var command = new AddSoftwareSkillCommand();
            User? user = new User();
            SystemSoftwareSkill? systemSoftwareSkill = new SystemSoftwareSkill() { Title = "Fake Software Skill" };
            Resume? resume = new Resume() { User = user, Code = string.Empty };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).Returns(systemSoftwareSkill);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeService.AddSoftwareSkill(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.AddAsync(A<SoftwareSkill>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }


    }
}

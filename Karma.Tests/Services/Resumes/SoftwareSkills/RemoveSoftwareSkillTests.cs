using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Tests.Services.Resumes.SoftwareSkills
{
    public class RemoveSoftwareSkillTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RemoveSoftwareSkillTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Software_Skill_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            SoftwareSkill? softwareSkill = null;

            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.GetByIdAsync(id)).Returns(softwareSkill);

            //Act
            var act = async () => await _resumeService.RemoveSoftwareSkill(id);

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("مهارت نرم افزاری مورد نظر یافت نشد.");

            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.Remove(A<SoftwareSkill>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Must_Remove_Software_Skill()
        {
            //Arrange
            var id = Guid.NewGuid();
            SoftwareSkill softwareSkill = new SoftwareSkill() { SystemSoftwareSkill = null };

            //Act
            var act = async () => await _resumeService.RemoveSoftwareSkill(id);

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.Remove(A<SoftwareSkill>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }
    }
}

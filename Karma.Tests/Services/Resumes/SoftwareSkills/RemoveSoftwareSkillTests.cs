﻿using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Resumes.SoftwareSkills
{
    public class RemoveSoftwareSkillTests : ResumeServiceTests
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Software_Skill_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            SoftwareSkill? softwareSkill = null;

            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.GetByIdAsync(id)).Returns(softwareSkill);

            //Act
            var act = async () => await _resumeWiteService.RemoveSoftwareSkillAsync(id);

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
            var act = async () => await _resumeWiteService.RemoveSoftwareSkillAsync(id);
            
            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SoftwareSkillRepository.Remove(A<SoftwareSkill>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }
    }
}

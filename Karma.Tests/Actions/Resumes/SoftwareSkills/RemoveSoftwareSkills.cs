using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.SoftwareSkills
{
    public class RemoveSoftwareSkills
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public RemoveSoftwareSkills()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Remove_Software_Skill_Without_Error()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var act = async () => await _resumesController.RemoveSoftwareSkill(id);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync();
            A.CallTo(() => _resumeWriteService.RemoveSoftwareSkillAsync(id)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

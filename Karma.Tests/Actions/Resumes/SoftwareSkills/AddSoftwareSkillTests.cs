using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.SoftwareSkills
{
    public class AddSoftwareSkillTests
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public AddSoftwareSkillTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Add_Software_skill_When_Data_Is_Valid()
        {
            //Arrange
            var command = new AddSoftwareSkillCommand();

            //Act
            var act = async () => await _resumesController.AddSoftwareSkill(command);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync();
            A.CallTo(() => _resumeWriteService.AddSoftwareSkillAsync(command, A<Guid>._)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

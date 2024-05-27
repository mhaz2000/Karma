using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.SoftwareSkills
{
    public class GetSoftwareSkills
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetSoftwareSkills()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_Software_Skills()
        {
            //Arrange
            var expectedResult = new List<SoftwareSkillDTO>()
            {
                new SoftwareSkillDTO()
                {
                    SystemSoftwareSkill = new SystemSoftwareSkillDTO(){Title = "Fake Software skill"}
                }
            };

            A.CallTo(() => _resumeReadService.GetSoftwareSkills(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.SoftwareSkills();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

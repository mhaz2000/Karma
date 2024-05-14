using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes
{
    public class GetAboutMeTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetAboutMeTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_About_Me()
        {
            //Arrange
            var expectedResult = new AboutMeDTO()
            {
                MainJobTitle = "Fake Job Title",
                Description = "Fake Description",
                ImageId = Guid.NewGuid(),
                SocialMedias = new List<SocialMediaDTO>()
                {
                    new SocialMediaDTO()
                    {
                        Link = "Fake Link",
                        Type = SocialMediaType.GitHub
                    }
                }
            };

            A.CallTo(() => _resumeReadService.GetAboutMe(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.AboutMe();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

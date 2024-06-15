using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.Languages
{
    public class GetLanguagesTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetLanguagesTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_Languages()
        {
            //Arrange
            var expectedResult = new List<LanguageDTO>()
            {
                new LanguageDTO()
                {
                    SystemLanguage = new SystemLanguageDTO(){Title = "Fake Language"}
                }
            };

            A.CallTo(() => _resumeReadService.GetLanguagesAsync(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.Languages();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.Languages
{
    public class AddLanguageTests
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public AddLanguageTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Add_Language_When_Data_Is_Valid()
        {
            //Arrange
            var command = new AddLanguageCommand();

            //Act
            var act = async () => await _resumesController.AddLanguage(command);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync();
            A.CallTo(() => _resumeWriteService.AddLanguageAsync(command, A<Guid>._)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Karma.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.WorkSamples
{
    public class AddWorkSampleTests
    {

        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public AddWorkSampleTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Link_Is_Empty()
        {
            //Arrange
            var command = new AddWorkSampleCommand()
            {
                Link = string.Empty,
            };

            //Act
            var act = async () => await _resumesController.AddWorkSample(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("لینک نمونه کار الزامی است.");
            A.CallTo(() => _resumeWriteService.AddWorkSampleAsync(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Add_Work_Sample_When_Inputs_Are_Valid()
        {
            //Arrange
            var command = new AddWorkSampleCommand()
            {
                Link = "Fake Link",
            };

            //Act
            var act = async () => await _resumesController.AddWorkSample(command);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync<ValidationException>();
            A.CallTo(() => _resumeWriteService.AddWorkSampleAsync(command, A<Guid>._)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

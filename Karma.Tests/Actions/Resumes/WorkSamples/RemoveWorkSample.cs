using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Actions.Resumes.WorkSamples
{
    public class RemoveWorkSample
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public RemoveWorkSample()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Remove_Work_Sample_Without_Error()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var act = async () => await _resumesController.RemoveWorkSample(id);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync();
            A.CallTo(() => _resumeWriteService.RemoveWorkSampleAsync(id)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

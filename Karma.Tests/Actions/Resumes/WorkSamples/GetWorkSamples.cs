using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Actions.Resumes.WorkSamples
{
    public class GetWorkSamples
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetWorkSamples()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_Work_Samples()
        {
            //Arrange
            var expectedResult = new List<WorkSampleDTO>()
            {
                new WorkSampleDTO()
                {
                    Id = Guid.NewGuid(),
                    Link = "Fake Link"
                }
            };

            A.CallTo(() => _resumeReadService.GetWorkSamplesAsync(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.WorkSamples();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

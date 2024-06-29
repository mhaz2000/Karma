using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes
{
    public class GetUserResumeTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetUserResumeTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;
        }

        [Fact]
        public async Task Should_Get_User_Resume()
        {
            var expectedResult = new UserResumeDTO() { Code = "123", MainJobTitle = "Fake Main Job Title" };

            A.CallTo(() => _resumeReadService.GetUserResumeAsync(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.GetUserResume(Guid.NewGuid());
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);
        }
    }
}

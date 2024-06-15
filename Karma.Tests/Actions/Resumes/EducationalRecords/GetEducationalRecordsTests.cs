using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.EducationalRecord
{

    public class GetEducationalRecordsTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetEducationalRecordsTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_Educational_Records()
        {
            //Arrange
            var expectedResult = new List<EducationalRecordDTO>()
            {
                new EducationalRecordDTO()
                {
                    Major = new MajorDTO() { Title = "Fake Title"},
                    University = new UniversityDTO() { Title = "Fake Title"}
                }
            };

            A.CallTo(() => _resumeReadService.GetEducationalRecordsAsync(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.EducationalRecords();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

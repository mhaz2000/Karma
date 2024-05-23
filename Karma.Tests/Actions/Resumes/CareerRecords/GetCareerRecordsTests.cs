using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes.CareerRecords
{
    public class GetCareerRecordsTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetCareerRecordsTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_Career_Records()
        {
            //Arrange
            var expectedResult = new List<CareerRecordDTO>()
            {
                new CareerRecordDTO()
                {
                    City = new CityDTO() {Title = "Fake City"},
                    CompanyName = "Fake Company Name",
                    Country = new CountryDTO() { Title = "Fake Country" },
                    JobCategory = new JobCategoryDTO(){ Title = "Fake Job Category"},
                    JobTitle = "Fake Job Title"
                }
            };

            A.CallTo(() => _resumeReadService.GetCareerRecords(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.CareerRecords();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

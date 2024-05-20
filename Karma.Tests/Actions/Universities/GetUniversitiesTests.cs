using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Universities
{
    public class GetUniversitiesTests
    {
        private readonly IUniversityService _universityService;
        private readonly UniversitiesController _universitiesController;

        public GetUniversitiesTests()
        {
            _universityService = A.Fake<IUniversityService>();
            _universitiesController = new UniversitiesController(_universityService);
        }

        [Fact]
        public async Task Should_Get_Universities()
        {
            //Act
            var result = await _universitiesController.Get(new PageQuery());
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

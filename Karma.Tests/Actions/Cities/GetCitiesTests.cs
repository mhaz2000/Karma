using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Cities
{
    public class GetCitiesTests
    {
        private readonly ICityService _cityService;
        private readonly CitiesController _citiesController;

        public GetCitiesTests()
        {
            _cityService = A.Fake<ICityService>();  
            _citiesController = new CitiesController(_cityService);
        }

        [Fact]
        public async Task Should_Get_Cities()
        {
            //Act
            var result = await _citiesController.Get();
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

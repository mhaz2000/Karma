using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Countries
{
    public class GetCountriesTests
    {
        private readonly ICountryService _countryService;
        private readonly CountriesController _countriesController;

        public GetCountriesTests()
        {
            _countryService = A.Fake<ICountryService>();
            _countriesController = new CountriesController(_countryService);
        }

        [Fact]
        public async Task Should_Get_Countries()
        {
            //Act
            var result = await _countriesController.Get();
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

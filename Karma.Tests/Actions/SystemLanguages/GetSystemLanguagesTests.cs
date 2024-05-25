using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.SystemLanguages
{
    public class GetSystemLanguagesTests
    {

        private readonly ISystemLanguageService _systemLanguageService;
        private readonly LanguagesController _languagesController;
        public GetSystemLanguagesTests()
        {
            _systemLanguageService = A.Fake<ISystemLanguageService>();
            _languagesController = new LanguagesController(_systemLanguageService);
        }

        [Fact]
        public async Task Should_Get_System_Languages()
        {
            //Act
            var result = await _languagesController.GetLanguages();
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

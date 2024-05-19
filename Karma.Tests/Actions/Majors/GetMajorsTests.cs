using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Majors
{
    public class GetMajorsTests
    {
        private readonly IMajorService _majorService;
        private readonly MajorsController _majorsController;

        public GetMajorsTests()
        {
            _majorService = A.Fake<IMajorService>();
            _majorsController = new MajorsController(_majorService);
        }

        [Fact]
        public async Task Should_Get_Majors()
        {
            //Act
            var result = await _majorsController.Get(A.Fake<IPageQuery>());
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

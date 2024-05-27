using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.SystemSoftwareSkills
{
    public class GetSystemSoftwareSkillsTests
    {
        private readonly ISystemSoftwareSkillService _systemSoftwareSkillService;
        private readonly SoftwareSkillsController _softwareSkillsController;
        public GetSystemSoftwareSkillsTests()
        {
            _systemSoftwareSkillService = A.Fake<ISystemSoftwareSkillService>();
            _softwareSkillsController = new SoftwareSkillsController(_systemSoftwareSkillService);
        }

        [Fact]
        public async Task Should_Get_System_Languages()
        {
            //Act
            var result = await _softwareSkillsController.GetSoftwareSkills(new PageQuery());
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

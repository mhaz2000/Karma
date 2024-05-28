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

namespace Karma.Tests.Actions.Resumes.AdditionalSkills
{
    public class GetAdditionalSkillsTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetAdditionalSkillsTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_AdditionalSkills()
        {
            //Arrange
            var expectedResult = new List<AdditionalSkillDTO>()
            {
                new AdditionalSkillDTO()
                {
                    Title ="Fake Language"
                }
            };

            A.CallTo(() => _resumeReadService.GetAdditionalSkills(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.AdditionalSkills();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

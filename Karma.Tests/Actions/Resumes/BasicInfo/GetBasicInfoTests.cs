using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Actions.Resumes.BasicInfo
{
    public class GetBasicInfoTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetBasicInfoTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Should_Get_About_Me()
        {
            //Arrange
            var expectedResult = new BasicInfoDTO()
            {
                BirthDate = DateTime.Now,
                City = "Fake City",
                FirstName = "Fake First Name",
                LastName = "Fake Last Name",
                MaritalStatus= MaritalStatus.Unknown,
                MilitaryServiceStatus = MilitaryServiceStatus.Unknown,
                Telephone = "Fake Telephoe"
            };

            A.CallTo(() => _resumeReadService.GetBasicInfo(A<Guid>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.BasicInfo();
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

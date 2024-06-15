using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Actions.Resumes.AdditionalSkills
{
    public class AddAdditionalSkillTests
    { 
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public AddAdditionalSkillTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Title_Is_Empty ()
        {
            //Arrange
            var command = new AddAdditionalSkillCommand() { Title = string.Empty };

            //Act
            var act = async () => await _resumesController.AddAdditionalSkill(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("عنوان مهارت تکمیلی الزامی است.");
            A.CallTo(() => _resumeWriteService.AddAdditionalSkillAsync(command, A<Guid>._)).MustNotHaveHappened();

        }

        [Fact]
        public async Task Should_Add_Additional_Skill_When_Data_Is_Valid()
        {
            //Arrange
            var command = new AddAdditionalSkillCommand() { Title = "Fake Title"};

            //Act
            var act = async () => await _resumesController.AddAdditionalSkill(command);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync();
            A.CallTo(() => _resumeWriteService.AddAdditionalSkillAsync(command, A<Guid>._)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

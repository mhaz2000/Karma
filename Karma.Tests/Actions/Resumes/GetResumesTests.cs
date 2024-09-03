using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.Resumes
{
    public class GetResumesTests
    {
        private readonly IResumeReadService _resumeReadService;
        private readonly IResumeWriteService _resumeWriteService;

        private readonly ResumesController _resumesController;

        public GetResumesTests()
        {
            _resumeReadService = A.Fake<IResumeReadService>();
            _resumeWriteService = A.Fake<IResumeWriteService>();
            var context = Fixture.FakeControllerContext();

            _resumesController = new ResumesController(_resumeWriteService, _resumeReadService);
            _resumesController.ControllerContext = context;

        }

        [Fact]
        public async Task Must_Throw_Exception_When_Younger_Than_Filter_Is_Not_Valid()
        {
            //Arrange
            var command = new ResumeFilterCommand() { YoungerThan = DateTime.Now.AddDays(1) };

            //Act
            var act = async () => await _resumesController.GetResumes(new PageQuery(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای فیلتر سن صحیح نمی‌باشد.");
            A.CallTo(() => _resumeReadService.GetResumesAsync(A<PageQuery>._, command)).MustNotHaveHappened();

        }

        [Fact]
        public async Task Must_Throw_Exception_When_Older_Than_Filter_Is_Not_Valid()
        {
            //Arrange
            var command = new ResumeFilterCommand() { OlderThan = DateTime.Now.AddDays(1) };

            //Act
            var act = async () => await _resumesController.GetResumes(new PageQuery(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای فیلتر سن صحیح نمی‌باشد.");
            A.CallTo(() => _resumeReadService.GetResumesAsync(A<PageQuery>._, command)).MustNotHaveHappened();

        }

        [Fact]
        public async Task Must_Throw_Exception_When_Older_Than_Is_Less_Than_Younger_Than()
        {
            //Arrange
            var command = new ResumeFilterCommand() { OlderThan = DateTime.Now.AddDays(-2), YoungerThan = DateTime.Now.AddDays(-1) };

            //Act
            var act = async () => await _resumesController.GetResumes(new PageQuery(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای فیلتر سن صحیح نمی‌باشد.");
            A.CallTo(() => _resumeReadService.GetResumesAsync(A<PageQuery>._, command)).MustNotHaveHappened();

        }

        [Fact]
        public async Task Should_Get_Resumes()
        {
            //Arrange
            var expectedResult = new List<ResumeQueryDTO>()
            {
                new ResumeQueryDTO()
                {
                    Code = string.Empty,
                    JobTitle= string.Empty,
                }
            };

            A.CallTo(() => _resumeReadService.GetResumesAsync(A<PageQuery>._, A<ResumeFilterCommand>._)).Returns(expectedResult);

            //Act
            var response = await _resumesController.GetResumes(new PageQuery(), new ResumeFilterCommand());
            var result = (OkObjectResult)response;

            //Assert
            result.StatusCode.Should().Be(200);

            result.Value.Should().Be(expectedResult);

        }
    }
}

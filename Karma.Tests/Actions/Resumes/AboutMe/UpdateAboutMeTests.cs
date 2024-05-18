using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Karma.Core.Enums;

namespace Karma.Tests.Actions.Resumes.AboutMe
{
    public class UpdateAboutMeTests
    {
        private readonly ResumesController _controller;
        private readonly IResumeWriteService _resumeWriteService;
        private readonly IResumeReadService _resumeReadService;
        public UpdateAboutMeTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumeReadService = A.Fake<IResumeReadService>();

            _controller = new ResumesController(_resumeWriteService, _resumeReadService);

            var context = Fixture.FakeControllerContext();
            _controller.ControllerContext = context;
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Main_Job_Title_Is_Empty()
        {
            //Arrange
            var command = new UpdateAboutMeCommand()
            {
                MainJobTitle = string.Empty
            };

            //Act
            var act = async () => await _controller.UpdateAboutMe(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("عنوان شغلی الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateAboutMeAsync(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_SocialMedia_Link_Is_Empty()
        {
            //Arrange
            var command = new UpdateAboutMeCommand()
            {
                MainJobTitle = "Fake Job Title",
                SocialMedias = new List<SocialMediaCommand>
                {
                    new SocialMediaCommand() { Type = SocialMediaType.GitHub, Link = string.Empty }
                }
            };

            //Act
            var act = async () => await _controller.UpdateAboutMe(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("آدرس شبکه اجتماعی الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateAboutMeAsync(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Update_About_Me()
        {
            //Arrange
            var command = new UpdateAboutMeCommand()
            {
                MainJobTitle = "Fake Job Title",
                SocialMedias = new List<SocialMediaCommand>
                {
                    new SocialMediaCommand() { Type = SocialMediaType.GitHub, Link = "Fake Link" }
                }
            };

            //Act
            var act = async () => await _controller.UpdateAboutMe(command);

            //Assert
            await act.Should().NotThrowAsync<ValidationException>();
            A.CallTo(() => _resumeWriteService.UpdateAboutMeAsync(command, A<Guid>._)).MustHaveHappened();
        }
    }
}

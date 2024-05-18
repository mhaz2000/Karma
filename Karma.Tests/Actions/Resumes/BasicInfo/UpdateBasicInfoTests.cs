using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;

namespace Karma.Tests.Actions.Resumes.BasicInfo
{
    public class UpdateBasicInfoTests
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public UpdateBasicInfoTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_First_Name_Is_Empty()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = "Fake City", FirstName = string.Empty, LastName = "Fake Last Name", BirthDate = DateTime.UtcNow.AddDays(-1) };

            //Act
            var act = async () => await _resumesController.UpdateBasicInfo(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("نام الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateBasicInfo(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Last_Name_Is_Empty()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = "Fake City", FirstName = "Fake First Name", LastName = string.Empty, BirthDate = DateTime.UtcNow.AddDays(-1) };

            //Act
            var act = async () => await _resumesController.UpdateBasicInfo(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("نام خانوادگی الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateBasicInfo(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_City_Is_Empty()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = string.Empty, FirstName = "Fake First Name", LastName = "Fake Last Name", BirthDate = DateTime.UtcNow.AddDays(-1) };

            //Act
            var act = async () => await _resumesController.UpdateBasicInfo(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("شهر الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateBasicInfo(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Birth_Date_Is_Invalid()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = "Fake City", FirstName = "Fake First Name", LastName = "Fake Last Name", BirthDate = DateTime.UtcNow.AddDays(1) };

            //Act
            var act = async () => await _resumesController.UpdateBasicInfo(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("تاریخ تولد صحیح نیست.");
            A.CallTo(() => _resumeWriteService.UpdateBasicInfo(command, A<Guid>._)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Should_Update_Basic_Info_When_Inputs_Are_Correct()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = "Fake City", FirstName = "Fake First Name", LastName = "Fake Last Name", BirthDate = DateTime.UtcNow.AddDays(-1) };

            //Act
            var act = async () => await _resumesController.UpdateBasicInfo(command);

            //Assert
            await act.Should().NotThrowAsync<ValidationException>();
            A.CallTo(() => _resumeWriteService.UpdateBasicInfo(command, A<Guid>._)).MustHaveHappened();
        }
    }
}
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Karma.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Karma.Tests.Actions.Resumes.CareerRecords
{
    public class UpdateCareerRecordTests
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public UpdateCareerRecordTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_CompanyName_Is_Empty()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = string.Empty,
                JobTitle = "Fake Job Title",
                CurrentJob = true,
                FromYear = 1400,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("نام سازمان الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Jobe_Title_Is_Empty()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = string.Empty,
                CurrentJob = true,
                FromYear = 1400,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("عنوان شغلی الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        [InlineData(5000)]
        public async Task Should_Throw_Exception_When_From_Year_Is_Invalid(int fromYear)
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = true,
                FromYear = fromYear,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_Is_Invalid()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = false,
                FromYear = 1400,
                ToYear = (new PersianCalendar()).GetYear(DateTime.Now.AddYears(1)),
                ToMonth = 5,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای سال پایان صحیح نیست.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_Is_Null_And_Current_Job_Is_False()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = false,
                FromYear = 1400,
                ToYear = null,
                ToMonth = 5,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("سال پایان الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Month_Is_Null_And_Current_Job_Is_False()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = false,
                FromYear = 1400,
                ToYear = 1401,
                ToMonth = null,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("ماه پایان الزامی است.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_Is_Not_Null_And_Current_Job_Is_True()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = true,
                FromYear = 1400,
                ToYear = 1401,
                ToMonth = null,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("سال پایان نمی‌تواند مقدار داشته باشد.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Month_Is_Not_Null_And_Current_Job_Is_True()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = true,
                FromYear = 1400,
                ToYear = null,
                ToMonth = 5,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("ماه پایان نمی‌تواند مقدار داشته باشد.");
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Add_Career_Record_When_Inputs_Are_Valid()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CurrentJob = true,
                FromYear = 1400,
                ToYear = null,
                ToMonth = null,
                FromMonth = 5,
                SeniorityLevel = SeniorityLevel.Employee
            };

            //Act
            var act = async () => await _resumesController.UpdateCareerRecord(Guid.NewGuid(), command);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync<ValidationException>();
            A.CallTo(() => _resumeWriteService.UpdateCareerRecord(command, A<Guid>._)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

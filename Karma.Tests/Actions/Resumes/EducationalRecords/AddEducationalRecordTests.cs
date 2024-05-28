using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Karma.API.Controllers;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Karma.Tests.Actions.Resumes.EducationalRecord
{
    public class AddEducationalRecordTests
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly ResumesController _resumesController;

        public AddEducationalRecordTests()
        {
            _resumeWriteService = A.Fake<IResumeWriteService>();
            _resumesController = new ResumesController(_resumeWriteService, A.Fake<IResumeReadService>());

            _resumesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Theory]
        [InlineData(21)]
        [InlineData(-2)]
        [InlineData(-5.6)]
        [InlineData(25.6)]
        public async Task Should_Throw_Exception_When_GPA_Is_Invalid(float gpa)
        {
            //Arrange
            var command = new AddEducationalRecordCommand() 
            {
                GPA = gpa,
                FromYear = 1399,
                ToYear = 1403
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای معدل صحیح نیست.");
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(5000)]
        public async Task Should_Throw_Exception_When_From_Year_Is_Not_Valid(int fromYear)
        {
            //Arrange
            var command = new AddEducationalRecordCommand()
            {
                GPA = 19,
                FromYear = fromYear,
                StillEducating = true
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_Is_Not_Valid()
        {
            //Arrange
            var command = new AddEducationalRecordCommand()
            {
                GPA = 19,
                FromYear = 1399,
                ToYear = (new PersianCalendar()).GetYear(DateTime.Now.AddYears(1))
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("مقدار وارد شده برای سال پایان صحیح نیست.");
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_Is_Less_Than_From_Year()
        {
            //Arrange
            var command = new AddEducationalRecordCommand()
            {
                GPA = 19,
                FromYear = 1399,
                ToYear = 1398
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("سال شروع نمی‌تواند از سال پایان بزرگتر باشد.");
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_And_Still_Educating_Are_Both_Null()
        {

            //Arrange
            var command = new AddEducationalRecordCommand()
            {
                GPA = 19,
                FromYear = 1399
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("سال پایان الزامی است.");
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustNotHaveHappened();
        }
        
        [Fact]
        public async Task Should_Throw_Exception_When_To_Year_And_Still_Educating_Are_has_Value()
        {
            //Arrange
            var command = new AddEducationalRecordCommand()
            {
                GPA = 19,
                FromYear = 1399,
                ToYear = 1400,
                StillEducating = true
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);

            //Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("سال پایان در حین تحصیل نمی‌تواند مقدار داشته باشد.");
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Should_Add_Educational_Record_When_Data_Is_Valid()
        {
            //Arrange
            var command = new AddEducationalRecordCommand()
            {
                GPA = 19,
                FromYear = 1399,
                ToYear = 1403,
            };

            //Act
            var act = async () => await _resumesController.AddEducationalRecord(command);
            var result = await act.Invoke();
            var response = (OkObjectResult)result;

            //Assert
            await act.Should().NotThrowAsync<ValidationException>();
            A.CallTo(() => _resumeWriteService.AddEducationalRecord(command, A<Guid>._)).MustHaveHappened();

            response.StatusCode.Should().Be(200);
        }
    }
}

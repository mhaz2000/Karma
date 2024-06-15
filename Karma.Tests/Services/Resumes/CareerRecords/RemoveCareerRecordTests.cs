using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Resumes.CareerRecords
{
    public class RemoveCareerRecordTests : ResumeServiceTests
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Career_Record_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            CareerRecord? careerRecord = null;

            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(id)).Returns(careerRecord);

            //Act
            var act = async () => await _resumeWiteService.RemoveCareerRecordAsync(id);

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("سابقه کاری مورد نظر یافت نشد.");

            A.CallTo(() => _unitOfWork.CareerRecordRepository.Remove(A<CareerRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Must_Remove_Career_Record()
        {
            //Arrange
            var id = Guid.NewGuid();
            CareerRecord careerRecord = new CareerRecord()
            {
                City = null,
                CompanyName = "Fake Company Name",
                Country = new Country() { Title = "Fake Country"},
                JobCategory = null,
                JobTitle = "Fake Job Title"
            };

            //Act
            var act = async () => await _resumeWiteService.RemoveCareerRecordAsync(id);

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.CareerRecordRepository.Remove(A<CareerRecord>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }

    }
}

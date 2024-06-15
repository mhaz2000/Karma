using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Resumes.WorkSamples
{
    public class UpdateWorkSampleTests : ResumeServiceTests
    {
        [Fact]
        public async Task Should_Throw_Exception_When_Work_Sample_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateWorkSampleCommand()
            {
                Link = "Fake Link"
            };

            WorkSample? workSample = null;

            A.CallTo(() => _unitOfWork.WorkSampleRepository.GetByIdAsync(A<Guid>._)).Returns(workSample);

            //Act
            var act = async () => await _resumeWiteService.UpdateWorkSampleAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.WorkSampleRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("نمونه کار مورد نظر یافت نشد.");
        }
        

        [Fact]
        public async Task Should_Update_Career_Record()
        {
            //Arrange
            var command = new UpdateWorkSampleCommand()
            {
                Link = "Fake Link"
            };

            WorkSample workSample = new WorkSample() { Link = "Fake Link" };

            A.CallTo(() => _unitOfWork.WorkSampleRepository.GetByIdAsync(A<Guid>._)).Returns(workSample);

            //Act
            var act = async () => await _resumeWiteService.UpdateWorkSampleAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.WorkSampleRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }

    }
}

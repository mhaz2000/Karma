using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Core.Entities;

namespace Karma.Tests.Services.Resumes.WorkSamples
{
    public class RemoveWorkSampleTests : ResumeServiceTests
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Work_Sample_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            WorkSample? workSample = null;

            A.CallTo(() => _unitOfWork.WorkSampleRepository.GetByIdAsync(id)).Returns(workSample);

            //Act
            var act = async () => await _resumeWiteService.RemoveWorkSampleAsync(id);

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("نمونه کار مورد نظر یافت نشد.");

            A.CallTo(() => _unitOfWork.WorkSampleRepository.Remove(A<WorkSample>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Must_Remove_Work_Sample()
        {
            //Arrange
            var id = Guid.NewGuid();
            WorkSample workSample = new WorkSample() { Link = "Fake Link" };

            //Act
            var act = async () => await _resumeWiteService.RemoveWorkSampleAsync(id);

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.WorkSampleRepository.Remove(A<WorkSample>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }

    }
}

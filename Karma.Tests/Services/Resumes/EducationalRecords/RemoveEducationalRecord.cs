using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Tests.Services.Resumes.EducationalRecords
{
    public class RemoveEducationalRecord
    {

        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RemoveEducationalRecord()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Educational_Record_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalRecord? educationalRecord = null;

            A.CallTo(() => _unitOfWork.EducationalRepository.GetByIdAsync(id)).Returns(educationalRecord);

            //Act
            var act = async () => await _resumeService.RemoveEducationalRecord(id);

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("سابقه تحصیلی مورد نظر یافت نشد.");

            A.CallTo(() => _unitOfWork.EducationalRepository.Remove(A<EducationalRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Must_Remove_Educational_Record()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalRecord educationalRecord = new EducationalRecord()
            {
                Major = new Major() { Title = "Fake Title" },
                University = new University() { Title = "Fake Title"}
            };

            //Act
            var act = async () => await _resumeService.RemoveEducationalRecord(id);

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.EducationalRepository.Remove(A<EducationalRecord>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }
    }
}

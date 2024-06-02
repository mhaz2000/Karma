using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.EducationalRecords
{
    public class UpdateEducationalRecordTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateEducationalRecordTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand();
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.GetByIdAsync(A<Guid>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Educational_Record_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand();
            User? user = new User();
            EducationalRecord educationalRecord = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).Returns(educationalRecord);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("رکورد تحصیلی مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Resume_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand();
            User? user = new User();
            Major major = new Major() { Title = "Fake Title" };
            University university = new University() { Title = "Fake Title" };
            EducationalRecord educationalRecord = new EducationalRecord() { Major = major, University = university };
            Resume resume = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).Returns(educationalRecord);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("رزومه شما یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Major_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand()
            {
                UniversityId = 1,
                MajorId = 1,
            };
            User? user = new User();
            Major major = new Major() { Title = "Fake Title" };
            University university = new University() { Title = "Fake Title" };
            EducationalRecord educationalRecord = new EducationalRecord() { Major = major, University = university };
            Resume resume = new Resume() { User = user, Code = string.Empty };
            Major newMajor = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).Returns(educationalRecord);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).Returns(newMajor);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای رشته دانشگاهی صحیح نمی‌باشد.");

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

        }

        [Fact]
        public async Task Should_Throw_Exception_When_University_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand()
            {
                UniversityId = 1,
                MajorId = 1,
            };
            User? user = new User();
            Major major = new Major() { Title = "Fake Title" };
            University university = new University() { Title = "Fake Title" };
            EducationalRecord educationalRecord = new EducationalRecord() { Major = major, University = university };
            Resume resume = new Resume() { User = user, Code = string.Empty };
            University newUniversity = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).Returns(educationalRecord);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).Returns(major);
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int?>._)).Returns(newUniversity);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای دانشگاه صحیح نمی‌باشد.");

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

        }

        [Fact]
        public async Task Should_Throw_Exception_When_Educational_Records_Have_Overlaps()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand()
            {
                UniversityId = 1,
                MajorId = 1,
                FromYear = 1399,
                ToYear = 1403
            };

            User? user = new User();
            Major major = new Major() { Title = "Fake Title" };
            University university = new University() { Title = "Fake Title" };
            EducationalRecord educationalRecord = new()
            {
                Major = major,
                University = university,
                FromYear = 1399,
                ToYear = 1403
            };

            Resume? resume = new Resume()
            {
                User = user,
                Code = string.Empty,
                EducationalRecords = new List<EducationalRecord>()
                {
                    educationalRecord,
                    new EducationalRecord()
                    {
                        Major = major,
                        University = university,
                        FromYear = 1399,
                        ToYear = 1403
                    }
                }
            };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).Returns(educationalRecord);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).Returns(major);
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int?>._)).Returns(university);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());

            //Assert
            await act.Should().ThrowAsync<ApplicationException>().WithMessage("سال های تدریس دانشگاه نمی‌تواند با هم تداخل زمانی داشته باشد.");

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

        }

        [Fact]
        public async Task Should_Update_Educational_Reocrd()
        {
            //Arrange
            var command = new UpdateEducationalRecordCommand()
            {
                UniversityId = 1,
                MajorId = 1,
            };

            User? user = new User();
            Major major = new Major() { Title = "Fake Title" };
            University university = new University() { Title = "Fake Title" };
            EducationalRecord educationalRecord = new EducationalRecord() { Major = major, University = university };
            Resume resume = new Resume() { User = user, Code = string.Empty };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).Returns(educationalRecord);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).Returns(major);
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int?>._)).Returns(university);

            //Act
            var act = async () => await _resumeService.UpdateEducationalRecord(Guid.NewGuid(), command, Guid.NewGuid());

            //Assert
            await act.Should().NotThrowAsync<ManagedException>();

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.MajorRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.UniversityRepository.GetByIdAsync(A<int?>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }
    }
}

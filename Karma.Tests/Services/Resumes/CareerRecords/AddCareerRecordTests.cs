using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.CareerRecords
{
    public class AddCareerRecordTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddCareerRecordTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new AddCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeService.AddCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume,bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CareerRecordRepository.AddAsync(A<CareerRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Job_Category_Cannot_Be_Found()
        {
            //Arrange
            var command = new AddCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = new User();
            JobCategory? jobCategory = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);

            //Act
            var act = async () => await _resumeService.AddCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CareerRecordRepository.AddAsync(A<CareerRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای زمینه فعالیت شما در این شرکت صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Country_Cannot_Be_Found()
        {
            //Arrange
            var command = new AddCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = new User();
            JobCategory? jobCategory = new JobCategory() { Title = "Fake Job Category Title"};
            Country? country = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).Returns(country);

            //Act
            var act = async () => await _resumeService.AddCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CareerRecordRepository.AddAsync(A<CareerRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای کشور صحیح نمی‌باشد.");
        }


        [Fact]
        public async Task Should_Throw_Exception_When_Country_Is_Iran_And_City_Cannot_Be_Found()
        {
            //Arrange
            var command = new AddCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = new User();
            JobCategory? jobCategory = new JobCategory() { Title = "Fake Job Category Title" };
            Country? country = new Country() { Title = "ایران" };
            City? city = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).Returns(country);
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).Returns(city);

            //Act
            var act = async () => await _resumeService.AddCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CareerRecordRepository.AddAsync(A<CareerRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار شهر الزامی است.");
        }

        [Fact]
        public async Task Should_Create_Resume_If_It_Does_Not_Exist()
        {
            //Arrange
            var command = new AddCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = new User();
            JobCategory? jobCategory = new JobCategory() { Title = "Fake Job Category"};
            Resume? resume = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);


            //Act
            var act = async () => await _resumeService.AddCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CareerRecordRepository.AddAsync(A<CareerRecord>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Should_Update_Resume_If_It_Exists()
        {
            //Arrange
            var command = new AddCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = new User();
            JobCategory? jobCategory = new JobCategory() { Title = "Fake Job Category" };
            Resume? resume = new Resume() { User = user};

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);


            //Act
            var act = async () => await _resumeService.AddCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.AddAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CareerRecordRepository.AddAsync(A<CareerRecord>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

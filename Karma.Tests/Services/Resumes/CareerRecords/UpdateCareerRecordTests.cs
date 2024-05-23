using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Services.Resumes.CareerRecords
{
    public class UpdateCareerRecordTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCareerRecordTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Career_Record_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            CareerRecord? careerRecord = null;

            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).Returns(careerRecord);

            //Act
            var act = async () => await _resumeService.UpdateCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("سابقه کاری مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Job_Category_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            User? user = new User();
            CareerRecord careerRecord = new CareerRecord() { City = null, CompanyName = "Fake Company Name", Country = null, JobCategory = null, JobTitle = "Fake Job Title"};
            JobCategory? jobCategory = null;

            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).Returns(careerRecord);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);

            //Act
            var act = async () => await _resumeService.UpdateCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();


            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای زمینه فعالیت شما در این شرکت صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Country_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title"
            };

            JobCategory? jobCategory = new JobCategory() { Title = "Fake Title"};
            CareerRecord careerRecord = new CareerRecord() { City = null, CompanyName = "Fake Company Name", Country = null, JobCategory = jobCategory, JobTitle = "Fake Job Title" };
            Country? country = null;

            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).Returns(careerRecord);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).Returns(country);

            //Act
            var act = async () => await _resumeService.UpdateCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای کشور صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Should_Update_Career_Record()
        {
            //Arrange
            var command = new UpdateCareerRecordCommand()
            {
                CompanyName = "Fake Company Name",
                JobTitle = "Fake Job Title",
                CityId = 1
            };

            JobCategory? jobCategory = new JobCategory() { Title = "Fake Title" };
            Country country = new Country() { Title = "Fake Title" };
            City city = new City() { Title = "Fake Title"};
            CareerRecord careerRecord = new CareerRecord() { City = city, CompanyName = "Fake Company Name", Country = country, JobCategory = jobCategory, JobTitle = "Fake Job Title" };

            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).Returns(careerRecord);
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).Returns(jobCategory);
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).Returns(country);
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(command.CityId)).Returns(city);

            //Act
            var act = async () => await _resumeService.UpdateCareerRecord(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.CareerRecordRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.JobCategoryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CountryRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CityRepository.GetByIdAsync(command.CityId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

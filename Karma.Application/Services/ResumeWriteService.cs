using AutoMapper;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Karma.Core.Entities;
using Karma.Core.EntityBuilders;
using Karma.Core.Repositories.Base;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Karma.Application.Services
{
    public class ResumeWriteService : IResumeWriteService
    {
        private const string Iran = "ایران";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ResumeWriteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task UpdateAboutMeAsync(UpdateAboutMeCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            user.ImageId = command.ImageId;
            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);

            if (existingResume is not null)
                _unitOfWork.SocialMediaRepository.RemoveRange(existingResume.SocialMedias);

            var resume = new ResumeBuilder(existingResume, user)
                .WithDescription(command.Description)
                .WithMainJobTitle(command.MainJobTitle)
                .WithSocialMedias(_mapper.Map<IEnumerable<SocialMedia>>(command.SocialMedias))
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);
            else
                await _unitOfWork.SocialMediaRepository.AddRangeAsync(resume.SocialMedias);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateBasicInfoAsync(UpdateBasicInfoCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            user.BirthDate = command.BirthDate;
            user.FirstName = command.FirstName;
            user.Email = command.Email;
            user.LastName = command.LastName;
            user.City = command.City;
            user.Telephone = command.Telephone;
            user.MaritalStatus = command.MaritalStatus;
            user.MilitaryServiceStatus = command.MilitaryServiceStatus;
            user.Gender = command.Gender;

            await _unitOfWork.CommitAsync();
        }

        public async Task AddEducationalRecordAsync(AddEducationalRecordCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var major = command.MajorId is not null ? (await _unitOfWork.MajorRepository.GetByIdAsync(command.MajorId) ??
                throw new ManagedException("مقدار وارد شده برای رشته دانشگاهی صحیح نمی‌باشد.")) : null;

            var university = command.UniversityId is not null ? (await _unitOfWork.UniversityRepository.GetByIdAsync(command.UniversityId) ??
                throw new ManagedException("مقدار وارد شده برای دانشگاه صحیح نمی‌باشد.")) : null;

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);

            var educationalRecord = _mapper.Map<EducationalRecord>(command);
            educationalRecord.Major = major;
            educationalRecord.University = university;

            var resume = new ResumeBuilder(existingResume, user)
                .WithEducationalRecords(educationalRecord)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.EducationalRecordRepository.AddAsync(educationalRecord);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateEducationalRecordAsync(Guid id, UpdateEducationalRecordCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var educationalRecord = await _unitOfWork.EducationalRecordRepository.GetByIdAsync(id) ??
                throw new ManagedException("رکورد تحصیلی مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user) ??
                throw new ManagedException("رزومه شما یافت نشد.");

            var major = command.MajorId is not null ? (await _unitOfWork.MajorRepository.GetByIdAsync(command.MajorId) ??
                throw new ManagedException("مقدار وارد شده برای رشته دانشگاهی صحیح نمی‌باشد.")) : null;

            var university = command.UniversityId is not null ? (await _unitOfWork.UniversityRepository.GetByIdAsync(command.UniversityId) ??
                throw new ManagedException("مقدار وارد شده برای دانشگاه صحیح نمی‌باشد.")) : null;

            educationalRecord.DegreeLevel = command.DegreeLevel;
            educationalRecord.ToYear = command.ToYear;
            educationalRecord.FromYear = command.FromYear;
            educationalRecord.Major = major;
            educationalRecord.University = university;
            educationalRecord.GPA = command.GPA;
            educationalRecord.StillEducating = command.StillEducating;

            resume = new ResumeBuilder(resume, user)
                .Build();

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveEducationalRecordAsync(Guid id)
        {
            var educationalRecord = await _unitOfWork.EducationalRecordRepository.GetByIdAsync(id);
            if (educationalRecord is null)
                throw new ManagedException("سابقه تحصیلی مورد نظر یافت نشد.");

            _unitOfWork.EducationalRecordRepository.Remove(educationalRecord);

            await _unitOfWork.CommitAsync();
        }

        public async Task AddCareerRecordAsync(AddCareerRecordCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var jobCategory = await _unitOfWork.JobCategoryRepository.GetByIdAsync(command.JobCategoryId) ??
                throw new ManagedException("مقدار وارد شده برای زمینه فعالیت شما در این شرکت صحیح نمی‌باشد.");

            var country = await _unitOfWork.CountryRepository.GetByIdAsync(command.CountryId) ??
                throw new ManagedException("مقدار وارد شده برای کشور صحیح نمی‌باشد.");

            var city = command.CityId is not null ? (await _unitOfWork.CityRepository.GetByIdAsync(command.CityId) ??
                throw new ManagedException("مقدار وارد شده برای شهر صحیح نمی‌باشد.")) : null;

            if (country.Title == Iran && city is null)
                throw new ManagedException("مقدار شهر الزامی است.");

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            var careerRecord = _mapper.Map<CareerRecord>(command);
            careerRecord.JobCategory = jobCategory;
            careerRecord.City = city;
            careerRecord.Country = country;

            var resume = new ResumeBuilder(existingResume, user)
                .WithCareerRecords(careerRecord)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.CareerRecordRepository.AddAsync(careerRecord);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateCareerRecordAsync(UpdateCareerRecordCommand command, Guid id)
        {
            var careerRecord = await _unitOfWork.CareerRecordRepository.GetByIdAsync(id) ??
                throw new ManagedException("سابقه کاری مورد نظر یافت نشد.");

            var jobCategory = await _unitOfWork.JobCategoryRepository.GetByIdAsync(command.JobCategoryId) ??
                throw new ManagedException("مقدار وارد شده برای زمینه فعالیت شما در این شرکت صحیح نمی‌باشد.");

            var country = await _unitOfWork.CountryRepository.GetByIdAsync(command.CountryId) ??
                throw new ManagedException("مقدار وارد شده برای کشور صحیح نمی‌باشد.");

            var city = command.CityId is not null ? (await _unitOfWork.CityRepository.GetByIdAsync(command.CityId) ??
                throw new ManagedException("مقدار وارد شده برای شهر صحیح نمی‌باشد.")) : null;

            if (country.Title == Iran && city is null)
                throw new ManagedException("مقدار شهر الزامی است.");

            careerRecord.SeniorityLevel = command.SeniorityLevel;
            careerRecord.ToYear = command.ToYear;
            careerRecord.ToMonth = command.ToMonth;
            careerRecord.FromMonth = command.FromMonth;
            careerRecord.CurrentJob = command.CurrentJob;
            careerRecord.City = city;
            careerRecord.CompanyName = command.CompanyName;
            careerRecord.Country = country;
            careerRecord.JobTitle = command.JobTitle;
            careerRecord.FromYear = command.FromYear;
            careerRecord.JobCategory = jobCategory;

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveCareerRecordAsync(Guid id)
        {
            var careerRecord = await _unitOfWork.CareerRecordRepository.GetByIdAsync(id);
            if (careerRecord is null)
                throw new ManagedException("سابقه کاری مورد نظر یافت نشد.");

            _unitOfWork.CareerRecordRepository.Remove(careerRecord);

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveLanguageAsync(Guid id)
        {
            var language = await _unitOfWork.LanguageRepository.GetByIdAsync(id) ??
                throw new ManagedException("زبان مورد نظر یافت نشد.");

            _unitOfWork.LanguageRepository.Remove(language);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddLanguageAsync(AddLanguageCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var systemLanguage = await _unitOfWork.SystemLanguageRepository.GetByIdAsync(command.LanguageId) ??
                throw new ManagedException("مقدار وارد شده برای زبان صحیح نمی‌باشد.");

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            var language = new Language() { SystemLanguage = systemLanguage, LanguageLevel = command.Level };

            var resume = new ResumeBuilder(existingResume, user)
                .WithLanguages(language)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.LanguageRepository.AddAsync(language);

            await _unitOfWork.CommitAsync();
        }

        public async Task AddSoftwareSkillAsync(AddSoftwareSkillCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var systemSofwareSkill = await _unitOfWork.SystemSoftwareSkillRepository.GetByIdAsync(command.SoftwareSkillId) ??
                throw new ManagedException("مقدار وارد شده برای مهارت نرم افزاری صحیح نمی‌باشد.");

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            var softwareSkill = new SoftwareSkill() { SystemSoftwareSkill = systemSofwareSkill, SoftwareSkillLevel = command.Level };

            var resume = new ResumeBuilder(existingResume, user)
                .WithSoftwareSkills(softwareSkill)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.SoftwareSkillRepository.AddAsync(softwareSkill);

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveSoftwareSkillAsync(Guid id)
        {
            var softwareSkill = await _unitOfWork.SoftwareSkillRepository.GetByIdAsync(id) ??
                throw new ManagedException("مهارت نرم افزاری مورد نظر یافت نشد.");

            _unitOfWork.SoftwareSkillRepository.Remove(softwareSkill);
            await _unitOfWork.CommitAsync();
        }

        public async Task AddAdditionalSkillAsync(AddAdditionalSkillCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            var additionalSkill = new AdditionalSkill() { Title = command.Title};

            var resume = new ResumeBuilder(existingResume, user)
                .WithAdditionalSkills(additionalSkill)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.AdditionalSkillRepository.AddAsync(additionalSkill);

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAdditionalSkillAsync(Guid id)
        {
            var additionalSkill = await _unitOfWork.AdditionalSkillRepository.GetByIdAsync(id) ??
                throw new ManagedException("مهارت نرم افزاری مورد نظر یافت نشد.");

            _unitOfWork.AdditionalSkillRepository.Remove(additionalSkill);
            await _unitOfWork.CommitAsync();
        }

        public async Task UploadPersonalResumeAsync(UploadPersonalResumeCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);

            var resume = new ResumeBuilder(existingResume, user)
                .WithResumeFileId(command.FileId)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.CommitAsync();

        }

        public async Task AddWorkSampleAsync(AddWorkSampleCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
               throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            var workSample = new WorkSample() { Link = command.Link };

            var resume = new ResumeBuilder(existingResume, user)
                .WithWorkSamples(workSample)
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.CreateAsync(resume);

            await _unitOfWork.WorkSampleRepository.AddAsync(workSample);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateWorkSampleAsync(UpdateWorkSampleCommand command, Guid id)
        {
            var workSample = await _unitOfWork.WorkSampleRepository.GetByIdAsync(id) ?? 
                throw new ManagedException("نمونه کار مورد نظر یافت نشد.");

            workSample.Link = command.Link;

            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveWorkSampleAsync(Guid id)
        {
            var workSample = await _unitOfWork.WorkSampleRepository.GetByIdAsync(id) ??
                throw new ManagedException("نمونه کار مورد نظر یافت نشد.");

            _unitOfWork.WorkSampleRepository.Remove(workSample);

            await _unitOfWork.CommitAsync();
        }
    }
}

﻿using AutoMapper;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Services.Interfaces;
using Karma.Core.EntityBuilders;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class ResumeReadService : IResumeReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ResumeReadService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<AboutMeDTO> GetAboutMeAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return new AboutMeDTO()
            {
                MainJobTitle = resume.MainJobTitle,
                Description = resume.Description,
                ImageId = user.ImageId,
                SocialMedias = _mapper.Map<IEnumerable<SocialMediaDTO>>(resume.SocialMedias)
            };
        }

        public async Task<BasicInfoDTO> GetBasicInfoAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            return _mapper.Map<BasicInfoDTO>(user);
        }

        public async Task<IEnumerable<CareerRecordDTO>> GetCareerRecordsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<CareerRecordDTO>>(resume.CareerRecords);
        }

        public async Task<IEnumerable<EducationalRecordDTO>> GetEducationalRecordsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<EducationalRecordDTO>>(resume.EducationalRecords);
        }

        public async Task<IEnumerable<LanguageDTO>> GetLanguagesAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<LanguageDTO>>(resume.Languages);
        }

        public async Task<IEnumerable<SoftwareSkillDTO>> GetSoftwareSkillsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<SoftwareSkillDTO>>(resume.SoftwareSkills);
        }

        public async Task<IEnumerable<AdditionalSkillDTO>> GetAdditionalSkillsAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<AdditionalSkillDTO>>(resume.AdditionalSkills);
        }

        public async Task<IEnumerable<ResumeQueryDTO>> GetResumesAsync(PageQuery pageQuery, ResumeFilterCommand command)
        {
            var resumes = await _unitOfWork.ExpandedResumeViewRepository.GetExpandedResumes();

            var filtredResumes = new ResumeQueryBuilder(resumes)
                .WithCareerExperienceLength(command.CareerExperienceLength)
                .WithCity(command.City)
                .WithMilitaryServiceStatuses(command.MilitaryServiceStatuses)
                .WithMaritalStatus(command.MaritalStatus)
                .WithCode(command.Code)
                .WithGender(command.Gender)
                .WithDegreeLevels(command.DegreeLevels)
                .WithJobCategories(command.JobCategoryIds)
                .WithLanguages(command.LanguageIds)
                .WithOlderThan(command.OlderThan)
                .WithYoungerThan(command.YoungerThan)
                .WithSoftwareSkill(command.SoftwareSkillIds)
                .Build();

            return _mapper.Map<IEnumerable<ResumeQueryDTO>>(filtredResumes)
                .OrderByDescending(c=>c.DegreeLevel)
                .DistinctBy(c => c.Id).ToPagingAndSorting(pageQuery);
        }

        public async Task<(FileStream stream, string filename)> DownloadPersonalResumeAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            if (resume.ResumeFileId is null)
                throw new ManagedException("رزومه شخصی بارگذاری نشده است.");

            return await _fileService.GetFileAsync(resume.ResumeFileId.Value);

        }

        public async Task<IEnumerable<WorkSampleDTO>> GetWorkSamplesAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<WorkSampleDTO>>(resume.WorkSamples);
        }

        public async Task<UserResumeDTO> GetUserResumeAsync(Guid id)
        {
            var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(id) ??
                throw new ManagedException("رزومه کاربر مورد نظر یافت نشد.");

            return _mapper.Map<UserResumeDTO>(resume);
        }
    }
}

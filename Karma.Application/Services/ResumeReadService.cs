using AutoMapper;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.EntityBuilders;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class ResumeReadService : IResumeReadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResumeReadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AboutMeDTO> GetAboutMe(Guid userId)
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

        public async Task<BasicInfoDTO> GetBasicInfo(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            return _mapper.Map<BasicInfoDTO>(user);
        }

        public async Task<IEnumerable<CareerRecordDTO>> GetCareerRecords(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<CareerRecordDTO>>(resume.CareerRecords);
        }

        public async Task<IEnumerable<EducationalRecordDTO>> GetEducationalRecords(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<EducationalRecordDTO>>(resume.EducationalRecords);
        }

        public async Task<IEnumerable<LanguageDTO>> GetLanguages(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<LanguageDTO>>(resume.Languages);
        }

        public async Task<IEnumerable<SoftwareSkillDTO>> GetSoftwareSkills(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<SoftwareSkillDTO>>(resume.SoftwareSkills);
        }

        public async Task<IEnumerable<AdditionalSkillDTO>> GetAdditionalSkills(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var resume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);
            if (resume is null)
                throw new ManagedException("رزومه شما یافت نشد.");

            return _mapper.Map<IEnumerable<AdditionalSkillDTO>>(resume.AdditionalSkills);
        }

        public async Task<IEnumerable<ResumeQueryDTO>> GetResumes(PageQuery pageQuery, ResumeFilterCommand command)
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

            return _mapper.Map<IEnumerable<ResumeQueryDTO>>(filtredResumes).Distinct();
        }
    }
}

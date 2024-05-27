using AutoMapper;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
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
    }
}

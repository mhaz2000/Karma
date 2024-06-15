﻿using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IResumeReadService
    {
        Task<AboutMeDTO> GetAboutMe(Guid userId);
        Task<BasicInfoDTO> GetBasicInfo(Guid userId);
        Task<IEnumerable<CareerRecordDTO>> GetCareerRecords(Guid userId);
        Task<IEnumerable<EducationalRecordDTO>> GetEducationalRecords(Guid userId);
        Task<IEnumerable<LanguageDTO>> GetLanguages(Guid userId);
        Task<IEnumerable<SoftwareSkillDTO>> GetSoftwareSkills(Guid userId);
        Task<IEnumerable<AdditionalSkillDTO>> GetAdditionalSkills(Guid userId);
        Task<IEnumerable<ResumeQueryDTO>> GetResumes(PageQuery pageQuery, ResumeFilterCommand command);
        Task<(FileStream stream, string filename)> DownloadPersonalResume(Guid userId);
    }
}

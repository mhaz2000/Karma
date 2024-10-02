using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IResumeReadService
    {
        Task<AboutMeDTO> GetAboutMeAsync(Guid userId);
        Task<BasicInfoDTO> GetBasicInfoAsync(Guid userId);
        Task<IEnumerable<CareerRecordDTO>> GetCareerRecordsAsync(Guid userId);
        Task<IEnumerable<EducationalRecordDTO>> GetEducationalRecordsAsync(Guid userId);
        Task<IEnumerable<LanguageDTO>> GetLanguagesAsync(Guid userId);
        Task<IEnumerable<SoftwareSkillDTO>> GetSoftwareSkillsAsync(Guid userId);
        Task<IEnumerable<AdditionalSkillDTO>> GetAdditionalSkillsAsync(Guid userId);
        Task<IEnumerable<ResumeQueryDTO>> GetResumesAsync(PageQuery pageQuery, ResumeFilterCommand command);
        Task<(FileStream stream, string filename)> DownloadPersonalResumeAsync(Guid userId);
        Task<IEnumerable<WorkSampleDTO>> GetWorkSamplesAsync(Guid userId);
        Task<UserResumeDTO> GetUserResumeAsync(Guid id);
        Task<MemoryStream> DownloadKarmaResumeAsync(Guid userId);
    }
}

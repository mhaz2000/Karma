using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IResumeReadService
    {
        Task<AboutMeDTO> GetAboutMe(Guid userId);
        Task<BasicInfoDTO> GetBasicInfo(Guid userId);
        Task<IEnumerable<EducationalRecordDTO>> GetEducationalRecords(Guid userId);
    }
}

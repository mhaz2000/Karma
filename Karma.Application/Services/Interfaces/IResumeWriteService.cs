using Karma.Application.Commands;

namespace Karma.Application.Services.Interfaces
{
    public interface IResumeWriteService
    {
        Task UpdateAboutMeAsync(UpdateAboutMeCommand command, Guid userId);
        Task UpdateBasicInfoAsync(UpdateBasicInfoCommand command, Guid userId);
        Task AddEducationalRecordAsync(AddEducationalRecordCommand command, Guid userId);
        Task UpdateEducationalRecordAsync(Guid id, UpdateEducationalRecordCommand command, Guid userId);
        Task RemoveEducationalRecordAsync(Guid id);
        Task AddCareerRecordAsync(AddCareerRecordCommand command, Guid userId);
        Task UpdateCareerRecordAsync(UpdateCareerRecordCommand command, Guid id);
        Task RemoveCareerRecordAsync(Guid id);
        Task RemoveLanguageAsync(Guid id);
        Task AddLanguageAsync(AddLanguageCommand command, Guid userId);
        Task AddSoftwareSkillAsync(AddSoftwareSkillCommand command, Guid userId);
        Task RemoveSoftwareSkillAsync(Guid id);
        Task AddAdditionalSkillAsync(AddAdditionalSkillCommand command, Guid userId);
        Task RemoveAdditionalSkillAsync(Guid id);
        Task UploadPersonalResumeAsync(UploadPersonalResumeCommand command, Guid userId);
        Task AddWorkSampleAsync(AddWorkSampleCommand command, Guid userId);
        Task UpdateWorkSampleAsync(UpdateWorkSampleCommand command, Guid id);
        Task RemoveWorkSampleAsync(Guid id);
    }
}

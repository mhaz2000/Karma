using Karma.Application.Commands;

namespace Karma.Application.Services.Interfaces
{
    public interface IResumeWriteService
    {
        Task UpdateAboutMeAsync(UpdateAboutMeCommand command, Guid userId);
        Task UpdateBasicInfo(UpdateBasicInfoCommand command, Guid userId);
        Task AddEducationalRecord(AddEducationalRecordCommand command, Guid userId);
        Task UpdateEducationalRecord(Guid id, UpdateEducationalRecordCommand command, Guid userId);
        Task RemoveEducationalRecord(Guid id);
    }
}

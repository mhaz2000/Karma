using Karma.Application.Commands;

namespace Karma.Application.Services.Interfaces
{
    public interface IResumeWriteService
    {
        Task UpdateAboutMeAsync(UpdateAboutMeCommand command, Guid userId);
    }
}

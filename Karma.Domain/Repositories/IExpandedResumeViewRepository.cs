using Karma.Core.ViewModels;

namespace Karma.Core.Repositories
{
    public interface IExpandedResumeViewRepository
    {
        Task<IQueryable<ExpandedResume>> GetExpandedResumes();
    }
}

using Karma.Core.Repositories;
using Karma.Core.ViewModels;
using Karma.Infrastructure.Data;

namespace Karma.Infrastructure.Repositories
{
    public class ExpandedResumeViewRepository : IExpandedResumeViewRepository
    {
        private readonly DataContext _dataContext;

        public ExpandedResumeViewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IQueryable<ExpandedResume>> GetExpandedResumes()
        {
            return await Task.FromResult(_dataContext.ExpandedResumesView);
        }
    }
}

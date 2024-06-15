using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class WorkSampleRepository : Repository<WorkSample>, IWorkSampleRepository
    {
        public WorkSampleRepository(DataContext context) : base(context)
        {
        }
    }
}

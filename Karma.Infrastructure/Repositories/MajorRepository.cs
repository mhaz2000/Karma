using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class MajorRepository : Repository<Major>, IMajorRepository
    {
        public MajorRepository(DataContext context) : base(context)
        {
        }
    }
}

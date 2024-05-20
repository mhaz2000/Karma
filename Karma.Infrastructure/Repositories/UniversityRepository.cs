using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class UniversityRepository : Repository<University>, IUniversityRepository
    {
        public UniversityRepository(DataContext context) : base(context)
        {
        }
    }
}

using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class EducationalRepository : Repository<EducationalRecord>, IEducationalRepository
    {
        public EducationalRepository(DataContext context) : base(context)
        {
        }
    }
}

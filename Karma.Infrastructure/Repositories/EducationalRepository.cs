using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class EducationalRecordRepository : Repository<EducationalRecord>, IEducationalRecordRepository
    {
        public EducationalRecordRepository(DataContext context) : base(context)
        {
        }
    }
}

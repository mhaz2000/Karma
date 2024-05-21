using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class CareerRecordRepository : Repository<CareerRecord>, ICareerRecordRepository
    {
        public CareerRecordRepository(DataContext context) : base(context)
        {
        }
    }
}

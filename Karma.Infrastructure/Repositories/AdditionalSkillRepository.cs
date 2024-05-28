using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class AdditionalSkillRepository : Repository<AdditionalSkill>, IAdditionalSkillRepository
    {
        public AdditionalSkillRepository(DataContext context) : base(context)
        {
        }
    }
}

using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class SoftwareSkillRepository : Repository<SoftwareSkill>, ISoftwareSkillRepository
    {
        public SoftwareSkillRepository(DataContext context) : base(context)
        {
        }
    }
}

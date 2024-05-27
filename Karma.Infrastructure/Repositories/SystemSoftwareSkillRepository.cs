using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class SystemSoftwareSkillRepository : Repository<SystemSoftwareSkill>, ISystemSoftwareSkillRepository
    {
        public SystemSoftwareSkillRepository(DataContext context) : base(context)
        {
        }
    }
}

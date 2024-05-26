using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class SystemLanguageRepository : Repository<SystemLanguage>, ISystemLanguageRepository
    {
        public SystemLanguageRepository(DataContext context) : base(context)
        {
        }
    }
}

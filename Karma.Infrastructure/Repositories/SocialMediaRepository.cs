using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class SocialMediaRepository : Repository<SocialMedia>, ISocialMediaRepository
    {
        public SocialMediaRepository(DataContext dataContext) : base(dataContext) 
        {
            
        }
    }
}

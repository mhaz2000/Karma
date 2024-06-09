using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Extensions;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class ResumeRepository : Repository<Resume>, IResumeRepository
    {
        public ResumeRepository(DataContext dataContext) : base(dataContext) 
        {
            
        }

        public async Task CreateAsync(Resume entity)
        {
            var codes = _dbSet.Select(s=>s.Code).ToList();
            var code = codes.Generate();

            entity.Code = code;
            await _dbSet.AddAsync(entity);
        }
    }
}

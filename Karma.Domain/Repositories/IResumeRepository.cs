using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Core.Repositories
{
    public interface IResumeRepository : IRepository<Resume>
    {
        Task CreateAsync(Resume entity);
    }
}

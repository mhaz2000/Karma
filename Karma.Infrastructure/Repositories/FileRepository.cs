using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class FileRepository : Repository<UploadedFile>, IFileRepository
    {
        public FileRepository(DataContext context) : base(context)
        {
        }
    }
}

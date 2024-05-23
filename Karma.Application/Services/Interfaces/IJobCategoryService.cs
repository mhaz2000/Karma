using Karma.Application.Base;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategoryDTO>> GetJobCategoriesAsync(string search);
    }
}

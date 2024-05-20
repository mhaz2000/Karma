using Karma.Application.Base;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IUniversityService
    {
        Task<IEnumerable<UniversityDTO>> GetUniversitiesAsync(PageQuery pageQuery, string search);
    }
}

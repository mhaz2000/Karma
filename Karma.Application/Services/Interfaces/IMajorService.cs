using Karma.Application.Base;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IMajorService
    {
        Task<IEnumerable<MajorDTO>> GetMajorsAsync(string search, IPageQuery pageQuery);
    }
}

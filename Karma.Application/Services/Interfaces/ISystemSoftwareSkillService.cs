
using Karma.Application.Base;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface ISystemSoftwareSkillService
    {
        Task<IEnumerable<SystemSoftwareSkillDTO>> GetSoftwareSkillsAsync(string search, IPageQuery pageQuery);
    }
}


using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface ISystemLanguageService
    {
        Task<IEnumerable<SystemLanguageDTO>> GetLanguages(string search);
    }
}

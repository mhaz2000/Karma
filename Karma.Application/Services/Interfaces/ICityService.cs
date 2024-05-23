
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetCities(string search);
    }
}

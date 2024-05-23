
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetCountries(string search);
    }
}

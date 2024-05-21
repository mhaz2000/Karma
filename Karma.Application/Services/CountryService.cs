using AutoMapper;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;
using System.Text.Json;

namespace Karma.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CountryDTO>> GetCountries(string search)
        {
            var countries = _unitOfWork.CountryRepository.Where(c => c.Title.Contains(search));
            return await Task.FromResult(_mapper.Map<IEnumerable<CountryDTO>>(countries));
        }
    }
}

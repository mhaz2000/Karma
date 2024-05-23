using AutoMapper;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetCities(string search)
        {
            var cities = _unitOfWork.CityRepository.Where(c => c.Title.Contains(search));
            return await Task.FromResult(_mapper.Map<IEnumerable<CityDTO>>(cities));
        }
    }
}

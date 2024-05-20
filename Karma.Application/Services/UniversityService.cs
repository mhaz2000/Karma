using AutoMapper;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UniversityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UniversityDTO>> GetUniversitiesAsync(PageQuery pageQuery, string search)
        {
            var result = _mapper.Map<IEnumerable<UniversityDTO>>(_unitOfWork.UniversityRepository.Where(c => c.Title.Contains(search)));
            return await Task.FromResult(result.ToPagingAndSorting(pageQuery));
        }
    }
}

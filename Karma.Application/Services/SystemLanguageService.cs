using AutoMapper;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class SystemLanguageService : ISystemLanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SystemLanguageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SystemLanguageDTO>> GetLanguages(string search)
        {
            var languages = _unitOfWork.SystemLanguageRepository.Where(c => c.Title.Contains(search));
            return await Task.FromResult(_mapper.Map<IEnumerable<SystemLanguageDTO>>(languages));
        }
    }
}

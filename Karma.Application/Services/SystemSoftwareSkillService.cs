using AutoMapper;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class SystemSoftwareSkillService : ISystemSoftwareSkillService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SystemSoftwareSkillService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SystemSoftwareSkillDTO>> GetSoftwareSkillsAsync(string search, IPageQuery pageQuery)
        {
            var softwareSkills = _unitOfWork.SystemSoftwareSkillRepository.Where(c => c.Title.Contains(search));
            return await Task.FromResult(_mapper.Map<IEnumerable<SystemSoftwareSkillDTO>>(softwareSkills).ToPagingAndSorting(pageQuery));
        }
    }
}

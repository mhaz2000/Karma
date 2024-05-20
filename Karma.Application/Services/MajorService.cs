using AutoMapper;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class MajorService : IMajorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MajorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MajorDTO>> GetMajorsAsync(string search, IPageQuery pageQuery)
        {
            var result = _mapper.Map<IEnumerable<MajorDTO>>(_unitOfWork.MajorRepository.Where(c => c.Title.Contains(search)));
            return  await Task.FromResult(result.ToPagingAndSorting(pageQuery));
        }
    }
}

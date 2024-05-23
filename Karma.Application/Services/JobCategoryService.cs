using AutoMapper;
using Karma.Application.DTOs;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JobCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobCategoryDTO>> GetJobCategoriesAsync(string search)
        {
            var result = _mapper.Map<IEnumerable<JobCategoryDTO>>(_unitOfWork.JobCategoryRepository.Where(c => c.Title.Contains(search)));
            return await Task.FromResult(result);
        }
    }
}

using AutoMapper;
using FakeItEasy;
using Karma.Application.Services;
using Karma.Application.Services.Interfaces;
using Karma.Core.Repositories.Base;

namespace Karma.Tests.Services.Resumes
{
    public class ResumeServiceTests
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IFileService _fileService;

        protected readonly ResumeReadService _resumeReadService;
        protected readonly ResumeWriteService _resumeWiteService;

        public ResumeServiceTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _mapper = A.Fake<IMapper>();
            _fileService = A.Fake<IFileService>();

            _resumeReadService = new ResumeReadService(_unitOfWork, _mapper, _fileService);
            _resumeWiteService = new ResumeWriteService(_unitOfWork, _mapper);
        }
    }
}

using AutoMapper;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Karma.Core.Entities;
using Karma.Core.EntityBuilders;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class ResumeWriteService : IResumeWriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ResumeWriteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task UpdateAboutMeAsync(UpdateAboutMeCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            user.ImageId = command.ImageId;
            var existingResume = await _unitOfWork.ResumeRepository.FirstOrDefaultAsync(c => c.User == user);

            if (existingResume is not null)
                _unitOfWork.SocialMediaRepository.RemoveRange(existingResume.SocialMedias);

            var resume = new ResumeBuilder(existingResume, user)
                .WithDescription(command.Description)
                .WithMainJobTitle(command.MainJobTitle)
                .WithSocialMedias(_mapper.Map<IEnumerable<SocialMedia>>(command.SocialMedias))
                .Build();

            if (existingResume is null)
                await _unitOfWork.ResumeRepository.AddAsync(resume);
            else
                await _unitOfWork.SocialMediaRepository.AddRangeAsync(resume.SocialMedias);

            await _unitOfWork.CommitAsync();
        }
    }
}

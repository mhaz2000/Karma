using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Tests.Services.Resumes.BasicInfo
{
    public class GetBasicInfoServiceTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ResumeReadService _resumeReadService;

        public GetBasicInfoServiceTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _mapper = A.Fake<IMapper>();

            _resumeReadService = new ResumeReadService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var userId = Guid.NewGuid();
            User user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);

            //Act
            var act = async () => await _resumeReadService.GetBasicInfo(userId);
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }


        [Fact]
        public async Task Should_Return_Basic_Data()
        {
            var userId = Guid.NewGuid();
            User user = new User();
            Resume? resume = new Resume() { User = user };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).Returns(user);

            //Act
            var act = async () => await _resumeReadService.GetBasicInfo(userId);
            var result = await act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId)).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync<ManagedException>();
        }
    }
}

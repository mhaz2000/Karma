using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Application.Services.Interfaces;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Services.Resumes.BasicInfo
{
    public class UpdateBasicInfoServiceTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBasicInfoServiceTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _resumeService = new ResumeWriteService(_unitOfWork, A.Fake<IMapper>());
        }

        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = "Fake City", FirstName = "Fake First Name", LastName = "Fake Last Name"};
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeService.UpdateBasicInfo(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Update_Basic_Info()
        {
            //Arrange
            var command = new UpdateBasicInfoCommand() { City = "Fake City", FirstName = "Fake First Name", LastName = "Fake Last Name" };
            User? user = new User();

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeService.UpdateBasicInfo(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync<ManagedException>();
        }
    }
}

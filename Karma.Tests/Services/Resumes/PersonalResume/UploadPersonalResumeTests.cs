﻿using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Core.Entities;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Resumes.PersonalResume
{
    public class UploadPersonalResumeTests : ResumeServiceTests
    {
        [Fact]
        public async Task Should_Throw_Exception_When_User_Cannot_Be_Found()
        {
            //Arrange
            var command = new UploadPersonalResumeCommand();
            User? user = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);

            //Act
            var act = async () => await _resumeWiteService.UploadPersonalResumeAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.ResumeRepository.CreateAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("کاربر مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Create_Resume_If_It_Deos_Not_Exist()
        {
            //Arrange
            var command = new UploadPersonalResumeCommand();
            User? user = new User();
            Resume? resume = null;

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeWiteService.UploadPersonalResumeAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.CreateAsync(A<Resume>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Should_Create_Resume_If_It_Exists()
        {
            //Arrange
            var command = new UploadPersonalResumeCommand();
            User? user = new User();
            Resume? resume = new Resume() { User = user, Code = string.Empty };

            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).Returns(user);
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).Returns(resume);

            //Act
            var act = async () => await _resumeWiteService.UploadPersonalResumeAsync(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UserRepository.GetActiveUserByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.FirstOrDefaultAsync(A<Expression<Func<Resume, bool>>>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.ResumeRepository.CreateAsync(A<Resume>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

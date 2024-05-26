using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Services.Resumes.Languages
{
    public class UpdateLanguageTests
    {
        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLanguageTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Language_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateLanguageCommand();
            Language? language = null;

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(A<Guid>._)).Returns(language);

            //Act
            var act = async () => await _resumeService.UpdateLanguage(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemLanguageRepository.GetByIdAsync(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("زبان مورد نظر یافت نشد.");
        }

        [Fact]
        public async Task Should_Throw_Exception_When_System_Language_Cannot_Be_Found()
        {
            //Arrange
            var command = new UpdateLanguageCommand();
            Language? language = new Language() { SystemLanguage = null};
            SystemLanguage? systemLanguage = null;

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(A<Guid>._)).Returns(language);
            A.CallTo(() => _unitOfWork.SystemLanguageRepository.GetByIdAsync(A<int>._)).Returns(systemLanguage);

            //Act
            var act = async () => await _resumeService.UpdateLanguage(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemLanguageRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();

            await act.Should().ThrowAsync<ManagedException>().WithMessage("مقدار وارد شده برای زبان صحیح نمی‌باشد.");
        }

        [Fact]
        public async Task Should_Update_Language()
        {
            //Arrange
            var command = new UpdateLanguageCommand();
            SystemLanguage? systemLanguage = new SystemLanguage() { Title = "Fake System Language"};
            Language? language = new Language() { SystemLanguage = systemLanguage };

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(A<Guid>._)).Returns(language);
            A.CallTo(() => _unitOfWork.SystemLanguageRepository.GetByIdAsync(A<int>._)).Returns(systemLanguage);

            //Act
            var act = async () => await _resumeService.UpdateLanguage(command, Guid.NewGuid());
            act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(A<Guid>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.SystemLanguageRepository.GetByIdAsync(A<int>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

            await act.Should().NotThrowAsync();
        }
    }
}

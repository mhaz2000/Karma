﻿using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Tests.Services.Resumes.Languages
{
    public class RemoveLanguageTests : ResumeServiceTests
    {
        [Fact]
        public async Task Must_Throw_Exception_When_Language_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            Language? language = null;

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(id)).Returns(language);

            //Act
            var act = async () => await _resumeWiteService.RemoveLanguageAsync(id);

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("زبان مورد نظر یافت نشد.");

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.LanguageRepository.Remove(A<Language>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Must_Remove_Language()
        {
            //Arrange
            var id = Guid.NewGuid();
            Language language = new Language() { SystemLanguage = null} ;

            //Act
            var act = async () => await _resumeWiteService.RemoveLanguageAsync(id);

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.LanguageRepository.Remove(A<Language>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }
    }
}

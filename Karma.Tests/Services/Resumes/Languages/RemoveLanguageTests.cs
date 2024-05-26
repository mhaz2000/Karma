using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Services.Resumes.Languages
{
    public class RemoveLanguageTests
    {

        private readonly ResumeWriteService _resumeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RemoveLanguageTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _resumeService = new ResumeWriteService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Must_Throw_Exception_When_Language_Cannot_Be_Found()
        {
            //Arrange
            var id = Guid.NewGuid();
            Language? language = null;

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(id)).Returns(language);

            //Act
            var act = async () => await _resumeService.RemoveLanguage(id);

            //Assert
            await act.Should().ThrowAsync<ManagedException>().WithMessage("زبان مورد نظر یافت نشد.");

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.EducationalRecordRepository.Remove(A<EducationalRecord>._)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task Must_Remove_Language()
        {
            //Arrange
            var id = Guid.NewGuid();
            Language language = new Language() { SystemLanguage = null} ;

            //Act
            var act = async () => await _resumeService.RemoveLanguage(id);

            //Assert
            await act.Should().NotThrowAsync();

            A.CallTo(() => _unitOfWork.LanguageRepository.GetByIdAsync(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.LanguageRepository.Remove(A<Language>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();

        }
    }
}

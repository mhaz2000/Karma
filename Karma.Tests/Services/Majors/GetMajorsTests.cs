using AutoMapper;
using FakeItEasy;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System.Linq.Expressions;

namespace Karma.Tests.Services.Majors
{
    public class GetMajorsTests
    {
        private readonly MajorService _majorService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMajorsTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _majorService = new MajorService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Must_Get_Majors()
        {
            //Arrange
            var expectedValue = new List<MajorDTO>()
            {
                new MajorDTO(),
                new MajorDTO(),
            };

            //Act
            var act = async () => await _majorService.GetMajors(string.Empty, A.Fake<IPageQuery>());
            var result = await act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.MajorRepository.Where(A<Expression<Func<Major, bool>>>._)).MustHaveHappenedOnceExactly();

        }
    }
}

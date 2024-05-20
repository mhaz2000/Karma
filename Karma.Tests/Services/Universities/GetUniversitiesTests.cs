using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Karma.Application.Base;
using Karma.Application.DTOs;
using Karma.Application.Services;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Tests.Services.Universities
{

    public class GetUniversitiesTests
    {
        private readonly UniversityService _universityService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUniversitiesTests()
        {
            _mapper = A.Fake<IMapper>();
            _unitOfWork = A.Fake<IUnitOfWork>();

            _universityService = new UniversityService(_unitOfWork, _mapper);
        }

        [Fact]
        public async Task Must_Get_Universities()
        {
            //Arrange
            var expectedValue = new List<UniversityDTO>()
            {
                new UniversityDTO() { Title = "Fake Title 1" },
                new UniversityDTO() { Title = "Fake Title 2" }
            };

            A.CallTo(() => _mapper.Map<IEnumerable<UniversityDTO>>(A<IQueryable<University>>._)).Returns(expectedValue);

            //Act
            var act = async () => await _universityService.GetUniversitiesAsync(A.Fake<PageQuery>(), string.Empty);
            var result = await act.Invoke();

            //Assert
            A.CallTo(() => _unitOfWork.UniversityRepository.Where(A<Expression<Func<University, bool>>>._)).MustHaveHappenedOnceExactly();

            result.Should().BeEquivalentTo(expectedValue);
        }
    }
}

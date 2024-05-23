using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Tests.Actions.JobCategories
{
    public class GetJobCategoriesTests
    {
        private readonly IJobCategoryService _jobCategoryService;
        private readonly JobCategoriesController _jobCategoreisController;

        public GetJobCategoriesTests()
        {
            _jobCategoryService = A.Fake<IJobCategoryService>();
            _jobCategoreisController = new JobCategoriesController(_jobCategoryService);
        }

        [Fact]
        public async Task Should_Get_Countries()
        {
            //Act
            var result = await _jobCategoreisController.Get();
            var response = (OkObjectResult)result;

            //Assert
            response.StatusCode.Should().Be(200);
        }
    }
}

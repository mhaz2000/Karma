using Karma.API.Controllers.Base;
using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoriesController : ApiControllerBase
    {
        private readonly IJobCategoryService _jobCategoryService;

        public JobCategoriesController(IJobCategoryService jobCategoryService)
        {
            _jobCategoryService = jobCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            var result = await _jobCategoryService.GetJobCategoriesAsync(search);

            return Ok(result);
        }
    }
}

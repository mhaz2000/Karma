using Karma.API.Controllers.Base;
using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ApiControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversitiesController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageQuery pageQuery, string search = "")
        {
            var result = await _universityService.GetUniversitiesAsync(pageQuery, search);

            return Ok(result);
        }
    }
}

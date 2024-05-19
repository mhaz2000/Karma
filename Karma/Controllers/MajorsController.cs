using Karma.API.Controllers.Base;
using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorsController : ApiControllerBase
    {
        private readonly IMajorService _majorService;

        public MajorsController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] IPageQuery pageQuery, string search = "")
        {
            var result = await _majorService.GetMajors(search, pageQuery);

            return Ok(result);
        }
    }
}

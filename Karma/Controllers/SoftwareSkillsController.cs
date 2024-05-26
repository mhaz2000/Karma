using Karma.API.Controllers.Base;
using Karma.Application.Base;
using Karma.Application.Services;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareSkillsController : ApiControllerBase
    {
        private readonly ISystemSoftwareSkillService _systemSoftwareSkillService;

        public SoftwareSkillsController(ISystemSoftwareSkillService systemSoftwareSkillService)
        {
            _systemSoftwareSkillService = systemSoftwareSkillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSoftwareSkills([FromQuery] PageQuery pageQuery, string search = "")
        {
            var result = await _systemSoftwareSkillService.GetSoftwareSkillsAsync(search, pageQuery);

            return Ok(result);
        }
    }
}

using Karma.API.Controllers.Base;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ApiControllerBase
    {
        private readonly IResumeWriteService _resumeWriteService;
        private readonly IResumeReadService _resumeReadService;

        public ResumesController(IResumeWriteService resumeWriteService, IResumeReadService resumeReadService)
        {
            _resumeWriteService = resumeWriteService;
            _resumeReadService = resumeReadService;
        }

        [HttpPut("AboutMe")]
        public async Task<IActionResult> UpdateAboutMe([FromBody] UpdateAboutMeCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateAboutMeAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("AboutMe")]
        public async Task<IActionResult> AboutMe()
        {
            var result = await _resumeReadService.GetAboutMe(UserId);

            return Ok(result);
        }
    }
}

using Karma.API.Controllers.Base;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        #region About Me

        [HttpPut("AboutMe")]
        public async Task<IActionResult> UpdateAboutMe([FromBody] UpdateAboutMeCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateAboutMeAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [AllowAnonymous]
        [HttpGet("AboutMe")]
        public async Task<IActionResult> AboutMe()
        {
            var result = await _resumeReadService.GetAboutMe(UserId);

            return Ok(result);
        }

        #endregion

        #region Basic Info

        [HttpPut("BasicInfo")]
        public async Task<IActionResult> UpdateBasicInfo([FromBody] UpdateBasicInfoCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateBasicInfo(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [AllowAnonymous]
        [HttpGet("BasicInfo")]
        public async Task<IActionResult> BasicInfo()
        {
            var result = await _resumeReadService.GetBasicInfo(UserId);

            return Ok(result);
        }

        #endregion

        #region Educational Records

        [HttpPut("AddEducationalRecord")]
        public async Task<IActionResult> AddEducationalRecord([FromBody] AddEducationalRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddEducationalRecord(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpPut("UpdateEducationalRecord/{id}")]
        public async Task<IActionResult> UpdateEducationalRecord(Guid id, [FromBody] UpdateEducationalRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateEducationalRecord(id, command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpDelete("RemoveEducationalRecord/{id}")]
        public async Task<IActionResult> RemoveEducationanlRecord(Guid id)
        {
            await _resumeWriteService.RemoveEducationalRecord(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [AllowAnonymous]
        [HttpGet("EducationalRecords")]
        public async Task<IActionResult> EducationalRecords()
        {
            var result = await _resumeReadService.GetEducationalRecords(UserId);

            return Ok(result);
        }

        #endregion

        #region Career Records

        [HttpPut("AddCareerRecord")]
        public async Task<IActionResult> AddCareerRecord([FromBody] AddCareerRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddCareerRecord(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion
    }
}

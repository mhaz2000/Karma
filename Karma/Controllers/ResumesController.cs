using Karma.API.Controllers.Base;
using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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

        [HttpGet("BasicInfo")]
        public async Task<IActionResult> BasicInfo()
        {
            var result = await _resumeReadService.GetBasicInfo(UserId);

            return Ok(result);
        }

        #endregion

        #region Educational Records

        [HttpPost("AddEducationalRecord")]
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

        [HttpGet("EducationalRecords")]
        public async Task<IActionResult> EducationalRecords()
        {
            var result = await _resumeReadService.GetEducationalRecords(UserId);

            return Ok(result);
        }

        #endregion

        #region Career Records

        [HttpPost("AddCareerRecord")]
        public async Task<IActionResult> AddCareerRecord([FromBody] AddCareerRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddCareerRecord(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpPut("UpdateCareerRecord/{id}")]
        public async Task<IActionResult> UpdateCareerRecord(Guid id, [FromBody] UpdateCareerRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateCareerRecord(command, id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("CareerRecords")]
        public async Task<IActionResult> CareerRecords()
        {
            var result = await _resumeReadService.GetCareerRecords(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveCareerRecord/{id}")]
        public async Task<IActionResult> RemoveCareerRecord(Guid id)
        {
            await _resumeWriteService.RemoveCareerRecord(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Languages

        [HttpPost("AddLanguage")]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddLanguage(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("Languages")]
        public async Task<IActionResult> Languages()
        {
            var result = await _resumeReadService.GetLanguages(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveLanguage/{id}")]
        public async Task<IActionResult> RemoveLanguage(Guid id)
        {
            await _resumeWriteService.RemoveLanguage(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Software skills

        [HttpPost("AddSoftwareSkill")]
        public async Task<IActionResult> AddSoftwareSkill([FromBody] AddSoftwareSkillCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddSoftwareSkill(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("SoftwareSkills")]
        public async Task<IActionResult> SoftwareSkills()
        {
            var result = await _resumeReadService.GetSoftwareSkills(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveSoftwareSkill/{id}")]
        public async Task<IActionResult> RemoveSoftwareSkill(Guid id)
        {
            await _resumeWriteService.RemoveSoftwareSkill(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Additional Skills

        [HttpPost("AddAdditionalSkill")]
        public async Task<IActionResult> AddAdditionalSkill([FromBody] AddAdditionalSkillCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddAdditionalSkill(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("AdditionalSkills")]
        public async Task<IActionResult> AdditionalSkills()
        {
            var result = await _resumeReadService.GetAdditionalSkills(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveAdditionalSkill/{id}")]
        public async Task<IActionResult> RemoveAdditionalSkill(Guid id)
        {
            await _resumeWriteService.RemoveAdditionalSkill(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Personal Resume

        [HttpPut("UploadPersonalResume")]
        public async Task<IActionResult> UploadPersonalResume([FromBody] UploadPersonalResumeCommand command)
        {
            await _resumeWriteService.UploadPersonalResume(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("DownloadPersonalResume")]
        public async Task<IActionResult> DownloadPersonalResume()
        {
            var file = await _resumeReadService.DownloadPersonalResume(UserId);
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(file.filename, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            Response.Headers.Append("Access-Control-Allow-Headers", "Content-Disposition");
            Response.Headers.Append("X-Content-Type-Options", "nosniff");

            return File(file.stream, contentType, file.filename);
        }

        #endregion

        [Authorize(Roles = "Admin")]
        [HttpPut("Query")]
        public async Task<IActionResult> GetResumes([FromQuery] PageQuery pageQuery, [FromBody] ResumeFilterCommand command)
        {
            command.Validate();

            var result = await _resumeReadService.GetResumes(pageQuery, command);
            return Ok(result);
        }
    }
}

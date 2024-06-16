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
            var result = await _resumeReadService.GetAboutMeAsync(UserId);

            return Ok(result);
        }

        #endregion

        #region Basic Information

        [HttpPut("BasicInfo")]
        public async Task<IActionResult> UpdateBasicInfo([FromBody] UpdateBasicInfoCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateBasicInfoAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("BasicInfo")]
        public async Task<IActionResult> BasicInfo()
        {
            var result = await _resumeReadService.GetBasicInfoAsync(UserId);

            return Ok(result);
        }

        #endregion

        #region Educational Records

        [HttpPost("AddEducationalRecord")]
        public async Task<IActionResult> AddEducationalRecord([FromBody] AddEducationalRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddEducationalRecordAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpPut("UpdateEducationalRecord/{id}")]
        public async Task<IActionResult> UpdateEducationalRecord(Guid id, [FromBody] UpdateEducationalRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateEducationalRecordAsync(id, command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpDelete("RemoveEducationalRecord/{id}")]
        public async Task<IActionResult> RemoveEducationanlRecord(Guid id)
        {
            await _resumeWriteService.RemoveEducationalRecordAsync(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("EducationalRecords")]
        public async Task<IActionResult> EducationalRecords()
        {
            var result = await _resumeReadService.GetEducationalRecordsAsync(UserId);

            return Ok(result);
        }

        #endregion

        #region Career Records

        [HttpPost("AddCareerRecord")]
        public async Task<IActionResult> AddCareerRecord([FromBody] AddCareerRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddCareerRecordAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpPut("UpdateCareerRecord/{id}")]
        public async Task<IActionResult> UpdateCareerRecord(Guid id, [FromBody] UpdateCareerRecordCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateCareerRecordAsync(command, id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("CareerRecords")]
        public async Task<IActionResult> CareerRecords()
        {
            var result = await _resumeReadService.GetCareerRecordsAsync(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveCareerRecord/{id}")]
        public async Task<IActionResult> RemoveCareerRecord(Guid id)
        {
            await _resumeWriteService.RemoveCareerRecordAsync(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Languages

        [HttpPost("AddLanguage")]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddLanguageAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("Languages")]
        public async Task<IActionResult> Languages()
        {
            var result = await _resumeReadService.GetLanguagesAsync(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveLanguage/{id}")]
        public async Task<IActionResult> RemoveLanguage(Guid id)
        {
            await _resumeWriteService.RemoveLanguageAsync(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Software skills

        [HttpPost("AddSoftwareSkill")]
        public async Task<IActionResult> AddSoftwareSkill([FromBody] AddSoftwareSkillCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddSoftwareSkillAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("SoftwareSkills")]
        public async Task<IActionResult> SoftwareSkills()
        {
            var result = await _resumeReadService.GetSoftwareSkillsAsync(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveSoftwareSkill/{id}")]
        public async Task<IActionResult> RemoveSoftwareSkill(Guid id)
        {
            await _resumeWriteService.RemoveSoftwareSkillAsync(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Additional Skills

        [HttpPost("AddAdditionalSkill")]
        public async Task<IActionResult> AddAdditionalSkill([FromBody] AddAdditionalSkillCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddAdditionalSkillAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("AdditionalSkills")]
        public async Task<IActionResult> AdditionalSkills()
        {
            var result = await _resumeReadService.GetAdditionalSkillsAsync(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveAdditionalSkill/{id}")]
        public async Task<IActionResult> RemoveAdditionalSkill(Guid id)
        {
            await _resumeWriteService.RemoveAdditionalSkillAsync(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        #region Personal Resume

        [HttpPut("UploadPersonalResume")]
        public async Task<IActionResult> UploadPersonalResume([FromBody] UploadPersonalResumeCommand command)
        {
            await _resumeWriteService.UploadPersonalResumeAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("DownloadPersonalResume")]
        public async Task<IActionResult> DownloadPersonalResume()
        {
            var file = await _resumeReadService.DownloadPersonalResumeAsync(UserId);
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

        #region Work Samples

        [HttpPost("AddWorkSample")]
        public async Task<IActionResult> AddWorkSample([FromBody] AddWorkSampleCommand command)
        {
            command.Validate();

            await _resumeWriteService.AddWorkSampleAsync(command, UserId);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpPut("UpdateWorkSample/{id}")]
        public async Task<IActionResult> UpdateWorkSample(Guid id, [FromBody] UpdateWorkSampleCommand command)
        {
            command.Validate();

            await _resumeWriteService.UpdateWorkSampleAsync(command, id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        [HttpGet("WorkSamples")]
        public async Task<IActionResult> WorkSamples()
        {
            var result = await _resumeReadService.GetWorkSamplesAsync(UserId);

            return Ok(result);
        }

        [HttpDelete("RemoveWorkSample/{id}")]
        public async Task<IActionResult> RemoveWorkSample(Guid id)
        {
            await _resumeWriteService.RemoveWorkSampleAsync(id);

            return Ok("تغییرات با موفقیت ثبت شد.");
        }

        #endregion

        [Authorize(Roles = "Admin")]
        [HttpPut("Query")]
        public async Task<IActionResult> GetResumes([FromQuery] PageQuery pageQuery, [FromBody] ResumeFilterCommand command)
        {
            command.Validate();

            var result = await _resumeReadService.GetResumesAsync(pageQuery, command);
            return Ok(result);
        }
    }
}

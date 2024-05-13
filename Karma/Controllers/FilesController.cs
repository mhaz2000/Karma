using Karma.API.Controllers.Base;
using Karma.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ApiControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload([FromQuery] IFormFile file)
        {
            using var stream = new MemoryStream();
            
            file.CopyTo(stream);

            var result = await _fileService.StoreFile(stream, UserId);

            return Ok(new { Message = "فایل با موفقیت باگذاری شد.", Value = result});
        }
    }
}

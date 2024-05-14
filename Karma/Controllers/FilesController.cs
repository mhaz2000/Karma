using Karma.API.Controllers.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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

            var result = await _fileService.StoreFileAsync(stream, UserId);

            return Ok(new { Message = "فایل با موفقیت باگذاری شد.", Value = result });
        }

        [HttpGet("Image/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var file = await _fileService.GetFileAsync(id);
            var fileStream = file.stream;
            var fileName = file.filename;

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            Response.Headers.Append("Access-Control-Allow-Headers", "Content-Disposition");
            Response.Headers.Append("X-Content-Type-Options", "nosniff");

            return File(fileStream, contentType, fileName);
        }
    }
}

using Karma.API.Controllers.Base;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ApiControllerBase
    {
        private readonly ISystemLanguageService _systemLanguageService;

        public LanguagesController(ISystemLanguageService systemLanguageService)
        {
            _systemLanguageService = systemLanguageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguages(string search="")
        {
            var result = await _systemLanguageService.GetLanguages(search);

            return Ok(result);
        }
    }
}

using Karma.Application.Commands;
using Karma.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            command.Validate();

            await _userService.Register(command);

            return Ok("ثبت نام با موفقیت انجام شد، کد");
        }

        [HttpPost("OtpLogin")]
        public async Task<IActionResult> OtpLogin(LoginCommand command)
        {
            command.Validate();

            await _userService.OtpLogin(command.Phone);

            return Ok();
        }
    }
}

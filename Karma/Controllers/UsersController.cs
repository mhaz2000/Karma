﻿using Karma.API.Controllers.Base;
using Karma.Application.Commands;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            command.Validate();

            await _userService.RegisterAsync(command);

            return Ok("ثبت نام با موفقیت انجام شد، کد ارسال شده به تلفن همراه خود را وارد نمایید.");
        }

        [AllowAnonymous]
        [HttpPost("OtpRequest")]
        public async Task<IActionResult> OtpRequest(OtpRequestCommand command)
        {
            command.Validate();

            await _userService.OtpRequestAsync(command);

            return Ok("کد تایید ارسال شد.");
        }

        [AllowAnonymous]
        [HttpPost("OtpLogin")]
        public async Task<IActionResult> OtpLogin(OtpLoginCommand command)
        {
            command.Validate();

            var result = await _userService.OtpLoginAsync(command);

            return Ok(new { Message = "با موفقیت وارد شدید.", Value = result });
        }

        [AllowAnonymous]
        [HttpPost("PhoneConfirmation")]
        public async Task<IActionResult> PhoneConfirmation(PhoneConfirmationCommand command)
        {
            command.Validate();

            var result = await _userService.PhoneConfirmationAsync(command);

            return Ok(new { Message = "تلفن همراه شما با موفقیت تایید شد.", Value = result });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            command.Validate();

            var result = await _userService.LoginAsync(command);

            return Ok(new { Message = "با موفقیت وارد شدید.", Value = result });
        }

        [HttpPost("SetPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordCommand command)
        {
            command.Validate();

            await _userService.SetPasswordAsync(command, UserId);

            return Ok("رمز عبور با موفقیت ثبت شد.");
        }

        [HttpGet("PasswordInitialized")]
        public async Task<IActionResult> CheckIfPasswordInitialized()
        {
            return Ok(await _userService.CheckIfPasswordInitializedAsync(UserId));
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
        {
            command.Validate();

            await _userService.UpdatePasswordAsync(command, UserId);

            return Ok("رمز عبور با موفقیت ویرایش شد.");
        }
    }
}

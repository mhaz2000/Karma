using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Helpers;
using Karma.Application.Services.Interfaces;
using Karma.Core.Caching;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Karma.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheProvider _cacheProvider;
        private readonly IAuthenticationHelper _authenticationHelper;

        private readonly Random _random;
        private readonly int _optExpirationTime;
        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, ICacheProvider cacheProvider, IAuthenticationHelper authenticationHelper)
        {
            _unitOfWork = unitOfWork;
            _random = new Random();
            _cacheProvider = cacheProvider;
            _authenticationHelper = authenticationHelper;

            _optExpirationTime = int.TryParse(configuration["InMemoryCaching:OptExpirationTime"]?.ToString(), out int expirationTime)
                ? expirationTime : throw new Exception("Opt expiration time cannot be found.");
        }

        public async Task<bool> CheckIfPasswordInitializedAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            return !string.IsNullOrEmpty(user.PasswordHash);
        }

        public async Task<AuthenticatedUserDTO> LoginAsync(LoginCommand command)
        {
            var user = await _unitOfWork.UserRepository
                .FirstOrDefaultAsync(c => (c.UserName == command.Username || c.PhoneNumber == command.Username || c.Email == command.Username) && c.PhoneNumberConfirmed);

            if (user is null)
                throw new ManagedException("نام کاربری یا رمز عبور اشتباه است.");

            if (!await _unitOfWork.UserRepository.CheckUserPasswordAsync(user, command.Password))
                throw new ManagedException("نام کاربری یا رمز عبور اشتباه است.");

            return await _authenticationHelper.GetToken(user);
        }

        public async Task<AuthenticatedUserDTO> OtpLoginAsync(OtpLoginCommand command)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(c => c.PhoneNumber == command.Phone && c.PhoneNumberConfirmed);
            if (user is null)
                throw new ManagedException("کاربری با این شماره تلفن یافت نشد.");

            if (command.OtpCode != await _cacheProvider.Get(command.Phone))
                throw new ManagedException("کد وارد شده صحیح نمی‌باشد.");

            return await _authenticationHelper.GetToken(user);
        }

        public async Task OtpRequestAsync(OtpRequestCommand command)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(c => c.PhoneNumber == command.Phone);
            if (user is null)
                throw new ManagedException("کاربری با این شماره تلفن یافت نشد.");

            if (await _cacheProvider.Get(command.Phone) is not null)
                throw new ManagedException("از مدت زمان درخواست کد تایید شما کمتر از 2 دقیقه گذشته است.");

            var otpCode = _random.Next(100000, 1000000);
            Console.WriteLine(otpCode); //while we don't have sms panel
            //Send Notification - Todo

            await _cacheProvider.Set(command.Phone, otpCode.ToString(), _optExpirationTime);
        }

        public async Task<AuthenticatedUserDTO> PhoneConfirmationAsync(PhoneConfirmationCommand command)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(c => c.PhoneNumber == command.Phone);
            if (user is null)
                throw new ManagedException("کاربری با این شماره تلفن یافت نشد.");

            if (command.OtpCode != await _cacheProvider.Get(command.Phone))
                throw new ManagedException("کد وارد شده صحیح نمی‌باشد.");

            user.PhoneNumberConfirmed = true;
            await _cacheProvider.Clear(command.Phone);

            var token = await _authenticationHelper.GetToken(user);
            await _unitOfWork.CommitAsync();

            return token;
        }

        public async Task RegisterAsync(RegisterCommand command)
        {
            command.Phone = command.Phone.ToDefaultFormat();

            var phoneDuplicationCheckCondition = await _unitOfWork.UserRepository.AnyAsync(c => c.PhoneNumber == command.Phone && c.PhoneNumberConfirmed);
            if (phoneDuplicationCheckCondition)
                throw new ManagedException("این شماره موبایل قبلا در سامانه ثبت شده است.");

            await _unitOfWork.UserRepository.CreateUserAsync(command.Phone);
            await _unitOfWork.CommitAsync();
        }

        public async Task SetPasswordAsync(SetPasswordCommand command, Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, command.Password);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePasswordAsync(UpdatePasswordCommand command, Guid userId)
        {
            var passwordHasher = new PasswordHasher<User>();
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(userId) ??
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                if (command.OldPassword is null)
                    throw new ManagedException("رمز عبور قبلی الزامی است.");
                else if (!await _unitOfWork.UserRepository.CheckUserPasswordAsync(user, command.OldPassword))
                    throw new ManagedException("رمز عبور قبلی صحیح نیست.");
            }

            user.PasswordHash = passwordHasher.HashPassword(user, command.NewPassword);

            await _unitOfWork.CommitAsync();
        }
    }
}
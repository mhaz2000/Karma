﻿using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.DTOs;
using Karma.Application.Extensions;
using Karma.Application.Helpers;
using Karma.Core.Caching;
using Karma.Core.Repositories.Base;
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

        public async Task<AuthenticatedUserDTO> OtpLogin(OtpLoginCommand command)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(c=> c.PhoneNumber == command.Phone);
            if (user is null)
                throw new ManagedException("کاربری با این شماره تلفن یافت نشد.");

            if (command.OtpCode != await _cacheProvider.Get(command.Phone))
                throw new ManagedException("کد وارد شده صحیح نمی‌باشد.");

            return await _authenticationHelper.GetToken(user);
        }

        public Task OtpRequest(OtpRequestCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task Register(RegisterCommand command)
        {
            command.Phone = command.Phone.ToDefaultFormat();

            var phoneDuplicationCheckCondition = await _unitOfWork.UserRepository.AnyAsync(c => c.PhoneNumber == command.Phone);
            if (phoneDuplicationCheckCondition)
                throw new ManagedException("این شماره موبایل قبلا در سامانه ثبت شده است.");

            var optCode = _random.Next(100000, 1000000);

            //Send Notification - Todo

            await _cacheProvider.Set(command.Phone, optCode.ToString(), _optExpirationTime);
            await _unitOfWork.UserRepository.CreateUserAsync(command.Phone);
            await _unitOfWork.CommitAsync();
        }
    }
}

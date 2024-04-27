﻿using Karma.Application.Base;
using Karma.Application.Helpers;
using Karma.Application.Helpers.TokenHelper;
using Karma.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Karma.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtIusserOptionsModel = new JwtIssuerOptionsModel();

            jwtIusserOptionsModel.Issuer = configuration.GetSection("JwtIssuerOptions").GetSection("Issuer").Value!;
            jwtIusserOptionsModel.SecretKey = configuration.GetSection("JwtIssuerOptions").GetSection("SecretKey").Value!;
            jwtIusserOptionsModel.Audience = configuration.GetSection("JwtIssuerOptions").GetSection("Audience").Value!;
            jwtIusserOptionsModel.ValidTimeInMinute = int.Parse(configuration.GetSection("JwtIssuerOptions").GetSection("ValidTimeInMinute").Value!);
            jwtIusserOptionsModel.ExpireTimeTokenInMinute = int.Parse(configuration.GetSection("JwtIssuerOptions").GetSection("ExpireTimeTokenInMinute").Value!);

            services.AddSingleton<JwtIssuerOptionsModel>(jwtIusserOptionsModel);

            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddScoped<ITokenFactory, TokenFactory>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
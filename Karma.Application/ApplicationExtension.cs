﻿using Karma.Application.Base;
using Karma.Application.Helpers;
using Karma.Application.Helpers.TokenHelper;
using Karma.Application.Notifications;
using Karma.Application.Services;
using Karma.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Karma.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtIusserOptionsModel = new JwtIssuerOptionsModel();
            var kavenegarConfigurationModel = new KavenegarConfigurationModel();

            jwtIusserOptionsModel.Issuer = configuration.GetSection("JwtIssuerOptions").GetSection("Issuer").Value!;
            jwtIusserOptionsModel.SecretKey = configuration.GetSection("JwtIssuerOptions").GetSection("SecretKey").Value!;
            jwtIusserOptionsModel.Audience = configuration.GetSection("JwtIssuerOptions").GetSection("Audience").Value!;
            jwtIusserOptionsModel.ValidTimeInMinute = int.Parse(configuration.GetSection("JwtIssuerOptions").GetSection("ValidTimeInMinute").Value!);
            jwtIusserOptionsModel.ExpireTimeTokenInMinute = int.Parse(configuration.GetSection("JwtIssuerOptions").GetSection("ExpireTimeTokenInMinute").Value!);


            kavenegarConfigurationModel.Template = configuration.GetSection("KavenegarConfiguration").GetSection("Template").Value!;
            kavenegarConfigurationModel.Key = configuration.GetSection("KavenegarConfiguration").GetSection("Key").Value!;

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton(jwtIusserOptionsModel);
            services.AddSingleton(kavenegarConfigurationModel);

            services.AddScoped<KavenegarFactory>();
            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddScoped<ITokenFactory, TokenFactory>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IMajorService, MajorService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IResumeReadService, ResumeReadService>();
            services.AddScoped<IUniversityService, UniversityService>();
            services.AddScoped<IResumeWriteService, ResumeWriteService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();
            services.AddScoped<ISystemLanguageService, SystemLanguageService>();
            services.AddScoped<ISystemSoftwareSkillService, SystemSoftwareSkillService>();

            var path = Directory.GetCurrentDirectory() + "/FileStorage";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return services;
        }
    }
}

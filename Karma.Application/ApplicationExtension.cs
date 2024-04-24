using Karma.Application.Base;
using Karma.Application.Helpers;
using Karma.Application.Helpers.TokenHelper;
using Karma.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Karma.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<JwtIssuerOptionsModel>(
                JsonConvert.DeserializeObject<JwtIssuerOptionsModel>(configuration["JwtIssuerOptions"]
                ?? throw new ArgumentException("Jwt issuer options cannot be found!"))!
                );

            services.AddScoped<ITokenFactory, TokenFactory>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

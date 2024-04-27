using Karma.Application.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Karma.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var jwtIssuerOptions = serviceProvider.GetRequiredService<JwtIssuerOptionsModel>();

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtIssuerOptions!.SecretKey));

            services.Configure<JwtIssuerOptionsModel>(options =>
            {
                options.Issuer = jwtIssuerOptions.Issuer;
                options.Audience = jwtIssuerOptions.Audience;
                options.SecretKey = jwtIssuerOptions.SecretKey;
                options.ExpireTimeTokenInMinute = jwtIssuerOptions.ExpireTimeTokenInMinute;
                options.ValidTimeInMinute = jwtIssuerOptions.ValidTimeInMinute;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtIssuerOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtIssuerOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtIssuerOptions.Issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Admin");
                    });

                options.AddPolicy("User",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("User");
                    });
            });

            return services;
        }
    }
}

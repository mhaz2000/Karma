﻿using Karma.Application.Base;
using Karma.Core.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Karma.Application.Helpers.TokenHelper
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptionsModel _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtFactory(IOptions<JwtIssuerOptionsModel> jwtOptions, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtOptions = jwtOptions.Value;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;

            ThrowIfInvalidOptions(_jwtOptions);
        }

        public string GenerateEncodedToken(User user, IList<string> userRoles, IEnumerable<string> roleIds, ClaimsIdentity identity)
        {
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                ClaimValueTypes.Integer64));
            identity.AddClaim(new Claim(ClaimTypes.Role, userRoles != null ? string.Join(',', userRoles) : ""));


            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: identity.Claims,
                notBefore: _jwtOptions.NotBefore,
                signingCredentials: _jwtOptions.SigningCredentials,
                expires: _jwtOptions.Expiration);

            var encodedJwt = _jwtSecurityTokenHandler.WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim("id", id),
                new Claim("rol", "api_access")
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round(
            (date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptionsModel options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidTimeInMinute <= 0)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptionsModel.ValidTimeInMinute));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptionsModel.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptionsModel.JtiGenerator));
            }
        }

    }
}

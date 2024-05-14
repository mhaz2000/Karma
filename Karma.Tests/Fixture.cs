using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Karma.Tests
{
    public static class Fixture
    {
        public static ControllerContext FakeControllerContext()
        {
            var fakeHttpContext = A.Fake<HttpContext>();
            var fakeHttpRequest = A.Fake<HttpRequest>();

            var requestBody = new MemoryStream();
            var writer = new StreamWriter(requestBody);
            writer.Write("Your request content here");
            writer.Flush();
            requestBody.Position = 0;

            A.CallTo(() => fakeHttpRequest.Body).Returns(requestBody);

            A.CallTo(() => fakeHttpRequest.Headers).Returns(
            new HeaderDictionary
            {
                {"Authorization", $"Bearer {GenerateRandomToken("T29vctFuIEFyZ2uhbSBGYW8hdmFyIFBhcmRpcw==", "Fake Issuer", "Fake Audience")}"}
            });

            A.CallTo(() => fakeHttpContext.Request).Returns(fakeHttpRequest);

            var context = new ControllerContext
            {
                HttpContext = fakeHttpContext,
            };

            return context;
        }

        private static string GenerateRandomToken(string secretKey, string issuer, string audience)
        {
            var claims = new[]
            {
                new Claim("UserId", Guid.NewGuid().ToString()),
            };

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: credentials
            );


            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}

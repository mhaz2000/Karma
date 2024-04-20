namespace Karma.API.Extensions
{
    public static class HttpRequestHelper
    {
        public static string GetAccessToken(this HttpRequest httpRequest) => httpRequest.Headers.ContainsKey("Authorization")
                                                                        ? httpRequest.Headers["Authorization"].ToString().Split(" ")[1]
                                                                        : string.Empty;
        public static string GetFullUrl(this HttpRequest httpRequest)
        {
            var url = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.Path}";
            return url;
        }
    }

}

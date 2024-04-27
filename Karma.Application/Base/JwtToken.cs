namespace Karma.Application.Base
{
    public class JsonWebTokenModel
    {
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}

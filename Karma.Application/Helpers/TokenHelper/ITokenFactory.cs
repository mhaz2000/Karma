namespace Karma.Application.Helpers.TokenHelper
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
    }
}

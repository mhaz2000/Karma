using Karma.Application.Commands;

namespace Karma.Application.Services
{
    public interface IUserService
    {
        Task OtpLogin(string phone);
        Task Register(RegisterCommand command);
    }
}

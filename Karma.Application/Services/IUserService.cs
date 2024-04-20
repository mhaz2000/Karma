using Karma.Application.Commands;

namespace Karma.Application.Services
{
    public interface IUserService
    {
        Task Register(RegisterCommand command);
    }
}

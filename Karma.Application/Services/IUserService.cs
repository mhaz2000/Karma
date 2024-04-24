using Karma.Application.Commands;
using Karma.Application.DTOs;

namespace Karma.Application.Services
{
    public interface IUserService
    {
        Task<AuthenticatedUserDTO> OtpLogin(OtpLoginCommand command);
        Task OtpRequest(OtpRequestCommand command);
        Task Register(RegisterCommand command);
    }
}

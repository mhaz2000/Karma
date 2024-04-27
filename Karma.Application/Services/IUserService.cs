using Karma.Application.Commands;
using Karma.Application.DTOs;

namespace Karma.Application.Services
{
    public interface IUserService
    {
        Task<AuthenticatedUserDTO> Login(LoginCommand command);
        Task<AuthenticatedUserDTO> OtpLogin(OtpLoginCommand command);
        Task OtpRequest(OtpRequestCommand command);
        Task<AuthenticatedUserDTO> PhoneConfirmation(PhoneConfirmationCommand command);
        Task Register(RegisterCommand command);
    }
}

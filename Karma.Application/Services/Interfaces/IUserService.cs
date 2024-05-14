using Karma.Application.Commands;
using Karma.Application.DTOs;

namespace Karma.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticatedUserDTO> LoginAsync(LoginCommand command);
        Task<AuthenticatedUserDTO> OtpLoginAsync(OtpLoginCommand command);
        Task OtpRequestAsync(OtpRequestCommand command);
        Task<AuthenticatedUserDTO> PhoneConfirmationAsync(PhoneConfirmationCommand command);
        Task RegisterAsync(RegisterCommand command);
    }
}

using Karma.Application.Base;
using Karma.Application.Commands;
using Karma.Application.Extensions;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Random _random = new Random();
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Register(RegisterCommand command)
        {
            command.Phone = command.Phone.ToFormat();

            var phoneDuplicationCheckCondition = await _unitOfWork.UserRepository.AnyAsync(c => c.PhoneNumber == command.Phone);
            if (phoneDuplicationCheckCondition)
                throw new ManagedException("این شماره موبایل قبلا در سامانه ثبت شده است.");

            var optCode = _random.Next(100000,1000000);

            //Send Notification - Todo

            await _unitOfWork.UserRepository.CreateUserAsync(command.Phone, optCode);
            await _unitOfWork.CommitAsync();
        }
    }
}

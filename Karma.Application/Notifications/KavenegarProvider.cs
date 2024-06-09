using Karma.Application.Base;
using Karma.Application.Notifications.Base;

namespace Karma.Application.Notifications
{
    public class KavenegarProvider : ISmsProvider
    {
        private readonly KavenegarConfigurationModel _config;

        public KavenegarProvider(KavenegarConfigurationModel config)
        {
            _config = config;
        }

        public async Task SendOtp(string code, string phone)
        {
            var api = new Kavenegar.KavenegarApi(_config.Key);
            api.VerifyLookup(phone, code, _config.Template);

            await Task.CompletedTask;
        }
    }
}

using Karma.Application.Base;
using Karma.Application.Notifications.Base;

namespace Karma.Application.Notifications
{
    public class KavenegarFactory : SmsProviderFactory
    {
        private readonly KavenegarConfigurationModel _config;

        public KavenegarFactory(KavenegarConfigurationModel config)
        {
            _config = config;
        }
        public override ISmsProvider Create()
        {
            return new KavenegarProvider(_config);
        }
    }
}

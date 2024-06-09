namespace Karma.Application.Notifications.Base
{
    public abstract class SmsProviderFactory
    {
        public abstract ISmsProvider Create();
    }
}

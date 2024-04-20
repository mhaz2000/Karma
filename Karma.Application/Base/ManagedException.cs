namespace Karma.Application.Base
{
    [Serializable]
    public class ManagedException : Exception
    {
        public ManagedException()
        {
        }

        public ManagedException(string message) : base(message)
        {
        }

        public ManagedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

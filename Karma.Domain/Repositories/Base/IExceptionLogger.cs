using Karma.Core.Entities.Base;

namespace Karma.Core.Repositories.Base
{
    public interface IExceptionLogger
    {
        Task LogAsync(ExceptionLog log);
    }
}

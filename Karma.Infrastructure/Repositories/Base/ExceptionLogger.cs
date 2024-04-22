using Karma.Core.Entities.Base;
using Karma.Core.Repositories.Base;
using Karma.Infrastructure.Data;

namespace Karma.Infrastructure.Repositories.Base
{
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly LogContext _context;

        public ExceptionLogger(LogContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task LogAsync(ExceptionLog log)
        {
            _context.ExceptionLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}

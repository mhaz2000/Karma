using Karma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Karma.Infrastructure.Factories.ContextsFactories
{
    internal class LogContextFactory
    {
        public LogContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new LogContext(optionsBuilder.Options);
        }
    }
}
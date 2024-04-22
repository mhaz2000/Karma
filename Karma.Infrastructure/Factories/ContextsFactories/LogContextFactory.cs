using Karma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Karma.Infrastructure.Factories.ContextsFactories
{
    internal class LogContextFactory
    {
        public LogContext CreateDbContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Log"));

            return new LogContext(optionsBuilder.Options);
        }
    }
}

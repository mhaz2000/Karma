using Karma.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Karma.Infrastructure.Factories.ContextsFactories
{
    internal class DateContextFactory
    {
        public DataContext CreateDbContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            return new DataContext(optionsBuilder.Options);
        }
    }
}

using Karma.Infrastructure.DbMigration;

namespace Karma.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase(this IHost host, string dbConnectionString, string logConnectionString)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var migrationService = serviceScope.ServiceProvider.GetRequiredService<IDatabaseMigration>();
                migrationService.MigrateDatabase(dbConnectionString, logConnectionString);
            }

            return host;
        }
    }
}

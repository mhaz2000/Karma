namespace Karma.Infrastructure.DbMigration
{
    public interface IDatabaseMigration
    {
        Task MigrateDatabase(string dbConnectionString, string logConnectionString);
    }
}

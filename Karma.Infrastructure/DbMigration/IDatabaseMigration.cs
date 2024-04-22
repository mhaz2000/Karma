namespace Karma.Infrastructure.DbMigration
{
    public interface IDatabaseMigration
    {
        Task MigrateDatabase();
    }
}

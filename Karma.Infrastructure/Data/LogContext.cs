using Karma.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Karma.Infrastructure.Data
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options)
             : base(options)
        {
        }

        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

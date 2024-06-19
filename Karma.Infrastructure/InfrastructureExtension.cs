using Karma.Core.Caching;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;
using Karma.Infrastructure.Caching;
using Microsoft.AspNetCore.Identity;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.DbMigration;
using Karma.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Karma.Infrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(dbConnectionString));

            // add identity
            var identityBuilder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.Tokens.ChangePhoneNumberTokenProvider = "Phone";
            });

            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole<Guid>), services);

            identityBuilder.AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IDatabaseMigration, DatabaseMigration>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExceptionLogger, ExceptionLogger>();
            services.AddScoped<ICacheProvider, InMemoryCaching>();

            return services;
        }
    }
}

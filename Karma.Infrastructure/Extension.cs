using Karma.Core.Caching;
using Karma.Core.Repositories.Base;
using Karma.Infrastructure.Caching;
using Karma.Infrastructure.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Karma.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheProvider, InMemoryCaching>();

            return services;
        }
    }
}

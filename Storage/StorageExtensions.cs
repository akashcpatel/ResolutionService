using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Connector.PostgreSql.EFCore;
using Storage.Implementations;

namespace Storage
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<StorageConfig>(config, StorageConfig.PositionInConfig);
            object p = services.AddHealthChecks().AddCheck<StorageHealthCheck>(nameof(StorageHealthCheck));

            services.AddDbContext<ResolutionDataContext>(options => 
            { 
                options.UseNpgsql(config);
            });

            services.RegisterServices();
            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IResolutionRepository, ResolutionRepository>();

            return services;
        }
    }
}

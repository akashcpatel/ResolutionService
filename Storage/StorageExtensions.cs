using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Connector.SqlServer.EFCore;
using Storage.Implementations;

namespace Storage
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<StorageConfig>(config, StorageConfig.PositionInConfig);

            services.AddHealthChecks().AddCheck<StorageHealthCheck>(nameof(StorageHealthCheck));

            services.AddDatabase(config);

            services.RegisterServices();
            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ResolutionDataContext>(options => options.UseSqlServer(config), ServiceLifetime.Singleton);

            var db = services.BuildServiceProvider().GetService<ResolutionDataContext>();
            db.Database.EnsureCreated();
            db.SaveChanges();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IResolutionRepository, ResolutionRepository>();

            return services;
        }
    }
}

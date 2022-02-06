using Application.HostedServices;
using Application.Implementations;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publisher;
using Services;
using Storage;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection SetupApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<ApplicationConfig>(config, ApplicationConfig.PositionInConfig);
            services.AddHealthChecks().AddCheck<ApplicationHealthCheck>(nameof(ApplicationHealthCheck));

            services.AddPublisher(config);

            services.AddStorage(config);

            services.AddServices(config);

            services.RegisterServices();

            services.AddHostedService<UserChangedReceiverService>();

            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IResolutionService, ResolutionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IApplicationServicesFactory, ServicesFactory>();
        }
    }
}

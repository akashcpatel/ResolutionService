using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publisher.Implementations;
using Publisher.Implementations.AsyncCommunicators;
using Steeltoe.Connector.RabbitMQ;

namespace Publisher
{
    public static class PublisherExtensions
    {
        public static IServiceCollection AddPublisher(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<PublisherConfig>(config, PublisherConfig.PositionInConfig);

            services.AddRabbitMQConnection(config);

            services.AddHealthChecks().AddCheck<PublisherHealthCheck>(nameof(PublisherHealthCheck));

            services.RegisterServices();

            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPublisherFactory, PublisherFactory>();
            services.AddScoped<IUserChangedReceiver, UserChangedReceiver>();
            services.AddScoped<IAsyncCommunicator, RabbitMQCommunicator>();
            services.AddScoped<IResolutionChangedPublisher, ResolutionChangedPublisher>();

            return services;
        }
    }
}

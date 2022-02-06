using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;

namespace Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<UserSearchConfig>(config, UserSearchConfig.PositionInConfig);

            services.AddHealthChecks().AddCheck<ServicesHealthCheck>(nameof(ServicesHealthCheck));

            services.RegisterServices();

            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<UserSearchAuthHttpClientHandler>();
            services.AddScoped<IRestClient, RestClient>();

            services.AddHttpClient<IUserSearch, UserSearch>()
                .GetRetryPolicy()
                .AddHttpMessageHandler<UserSearchAuthHttpClientHandler>();
        }
    }
}

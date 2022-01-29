using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddConfig<T>(this IServiceCollection services, IConfiguration config, string sectionName) where T : class
        {
            services.AddOptions<T>().Bind(config.GetSection(sectionName)).ValidateDataAnnotations();
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<T>>().Value);

            return services;
        }
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using Steeltoe.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Main
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureApp)
            .ConfigureServices(ConfigureServices)
            .ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                loggingBuilder.AddDynamicConsole();
            })
            .UseStartup<Startup>();

        private static void ConfigureServices(WebHostBuilderContext webHostBuilder, IServiceCollection services)
        {
            services.AddServices(webHostBuilder.Configuration);
        }

        private static void ConfigureApp(WebHostBuilderContext hostBuilder, IConfigurationBuilder configBuilder)
        {
            configBuilder.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment}.json", true, true)
                .AddEnvironmentVariables("RESOLUTIONSERVICE_");

            if (hostBuilder.HostingEnvironment.IsDevelopment())
                configBuilder.AddUserSecrets<Program>();
        }
    }
}

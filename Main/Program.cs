using Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;
using Steeltoe.Extensions.Logging;
using System.Threading.Tasks;

namespace Main
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureApp)
            .ConfigureServices(ConfigureServices)
            .ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                loggingBuilder.AddDynamicConsole();
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static void ConfigureServices(HostBuilderContext webHostBuilder, IServiceCollection services)
        {
            services.SetupApplication(webHostBuilder.Configuration);
        }

        private static void ConfigureApp(HostBuilderContext hostBuilder, IConfigurationBuilder configBuilder)
        {
            configBuilder.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment}.json", true, true)
                .AddEnvironmentVariables("RESOLUTIONSERVICE_");

            if (hostBuilder.HostingEnvironment.IsDevelopment())
                configBuilder.AddUserSecrets<Program>();
        }
    }
}

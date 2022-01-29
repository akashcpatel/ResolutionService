using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Logging;

namespace Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureApp)
            .ConfigureLogging((context, builder) => builder.AddDynamicConsole())
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

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

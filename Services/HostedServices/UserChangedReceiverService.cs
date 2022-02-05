using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Publisher;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.HostedServices
{
    internal class UserChangedReceiverService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ServicesConfig _servicesConfig;
        private readonly IPublisherFactory _publisherFactory;
        private readonly IServicesFactory _servicesFactory;

        public UserChangedReceiverService(ILogger<UserChangedReceiverService> logger, ServicesConfig servicesConfig, IPublisherFactory publisherFactory,
            IServicesFactory servicesFactory)
        {
            _publisherFactory = publisherFactory;
            _servicesFactory = servicesFactory;
            _logger = logger;
            _servicesConfig = servicesConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (true)
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var userChangedData = await _publisherFactory.GetUserChangedReceiver()?.Receive();

                if (userChangedData == null)
                    continue;

                var userService = _servicesFactory.GetUserService();
                await userService?.Sync(userChangedData);

                await Task.Delay(new TimeSpan(0, 0, _servicesConfig.UserChangedReceiverServiceDelayInSeconds));
            }
        }
    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Publisher;
using Publisher.Message.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.HostedServices
{
    internal class UserChangedReceiverService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ApplicationConfig _servicesConfig;
        private readonly IPublisherFactory _publisherFactory;
        private readonly IApplicationServicesFactory _servicesFactory;

        public UserChangedReceiverService(ILogger<UserChangedReceiverService> logger, ApplicationConfig servicesConfig, IPublisherFactory publisherFactory,
            IApplicationServicesFactory servicesFactory)
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

                UserChangedData userChangedData = null;

                try
                {
                    userChangedData = await _publisherFactory.GetUserChangedReceiver()?.Receive();
                    await _servicesFactory.GetUserService()?.Sync(userChangedData);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occurred processing user = {user}", userChangedData, ex);
                }
                finally
                {
                    await Task.Delay(new TimeSpan(0, 0, _servicesConfig.UserChangedReceiverServiceDelayInSeconds));
                }
            }
        }
    }
}

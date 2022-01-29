using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Publisher;
using System.Threading;
using System.Threading.Tasks;

namespace Services.HostedServices
{
    internal class UserChangedReceiverService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IUserChangedReceiver _userChangedReceiver;

        public UserChangedReceiverService(ILogger<UserChangedReceiverService> logger,
            IUserChangedReceiver userChangedReceiver)
        {
            _logger = logger;
            _userChangedReceiver = userChangedReceiver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var userChangedData = await _userChangedReceiver.Receive();
                if(userChangedData != null)
                {
                    //TODO: Process user changed data.
                }

                await Task.Delay(5000);
            }
        }
    }
}

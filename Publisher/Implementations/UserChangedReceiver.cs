using Publisher.Message.Data;
using System.Threading.Tasks;

namespace Publisher.Implementations
{
    internal class UserChangedReceiver : IUserChangedReceiver
    {
        private readonly PublisherConfig _config;
        private readonly IAsyncCommunicator _communicator;

        public UserChangedReceiver(PublisherConfig config, IAsyncCommunicator communicator)
        {
            _config = config;
            _communicator = communicator;
        }

        public async Task<UserChangedData> Receive()
        {
            return await _communicator.Receive<UserChangedData>(_config.UserChangedQueue);
        }
    }
}

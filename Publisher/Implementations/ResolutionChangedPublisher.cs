using Model;
using Newtonsoft.Json;
using Publisher.Message;
using Publisher.Message.Data;
using System;
using System.Threading.Tasks;

namespace Publisher.Implementations
{
    internal class ResolutionChangedPublisher : IResolutionChangedPublisher
    {
        private readonly IAsyncCommunicator _asyncCommunicator;
        private readonly PublisherConfig _config;

        public ResolutionChangedPublisher(IAsyncCommunicator asyncCommunicator, PublisherConfig config)
        {
            _asyncCommunicator = asyncCommunicator;
            _config = config;
        }

        public async Task Add(Resolution r)
        {
            await _asyncCommunicator.Send(_config.ResolutionChangedQueue, CreateMessage(r, ChangeType.Add));
        }

        public async Task Delete(Guid id)
        {
            await _asyncCommunicator.Send(_config.ResolutionChangedQueue, CreateMessage(new Resolution { Id = id }, ChangeType.Delete));
        }

        public async Task Update(Resolution r)
        {
            await _asyncCommunicator.Send(_config.ResolutionChangedQueue, CreateMessage(r, ChangeType.Update));
        }

        private string CreateMessage(Resolution r, ChangeType changeType)
        {
            var resolutionChangedData = ResolutionChangedData.Create(Header.Create(Guid.NewGuid(), changeType), r);
            var message = JsonConvert.SerializeObject(resolutionChangedData);
            return JsonConvert.SerializeObject(resolutionChangedData);
        }
    }
}

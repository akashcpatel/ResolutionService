using Microsoft.Extensions.Logging;
using Model;
using Publisher;
using Storage;
using System;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class ResolutionService : IResolutionService
    {
        private readonly ILogger<ResolutionService> _logger;
        private readonly IResolutionRepository _resolutionRepository;
        private readonly IResolutionChangedPublisher _publisher;
        private readonly ServicesConfig _config;

        public ResolutionService(ILogger<ResolutionService> logger, ServicesConfig config,
            IResolutionChangedPublisher publisher, IResolutionRepository resolutionRepository)
        {
            _logger = logger;
            _config = config;
            _resolutionRepository = resolutionRepository;
            _publisher = publisher;
        }

        public async Task<Guid?> UpSert(Resolution r)
        {
            _logger.LogInformation("UpSert resolution = {resolution}", r);
            var user = await _resolutionRepository.Find(r.Id);
            if (user == null)
                await Add(r);
            else
                await Update(r);

            _logger.LogInformation("UpSert resolution completed for {resolution}", r);
            return await Task.FromResult(r?.Id);
        }

        public async Task<bool> Delete(Guid id)
        {
            _logger.LogInformation("Delete resolution {id}", id);

            await _resolutionRepository.Delete(id);
            _ = Task.Run(async () => await _publisher.Delete(id));

            _logger.LogInformation("Delete resolution completed for id = {id}", id);
            return await Task.FromResult(true);
        }

        public async Task<Resolution> Find(Guid id)
        {
            _logger.LogInformation("Find resolution for id = {id}.", id);

            var user = await _resolutionRepository.Find(id);

            if (user == null)
                _logger.LogInformation("Did not find resolution for id = {id}.", id);
            else
                _logger.LogInformation("Found user {resolution} for id = {id}.", user, id);

            return user;
        }

        private async Task Add(Resolution r)
        {
            _logger.LogInformation("Add resolution {resolution}", r);

            await _resolutionRepository.Add(r);
            _ = Task.Run(async () => await _publisher.Add(r));
        }

        private async Task Update(Resolution r)
        {
            _logger.LogInformation("Update resolution {resolution}", r);

            await _resolutionRepository.Update(r);
            _ = Task.Run(async () => await _publisher.Update(r));
        }
    }
}

using Microsoft.Extensions.Logging;
using Model;
using Publisher.Message;
using Publisher.Message.Data;
using Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IResolutionService _resolutionService;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IResolutionService resolutionService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _resolutionService = resolutionService;

            CreateActions();
        }

        private void CreateActions()
        {
            _actions.Add(ChangeType.Add, Save);
            _actions.Add(ChangeType.Update, Save);
            _actions.Add(ChangeType.Delete, DeleteUser);
        }

        public async Task<User> Find(Guid id)
        {
            return await _userRepository.Find(id);
        }

        private readonly Dictionary<ChangeType, Func<User, Task>> _actions = new Dictionary<ChangeType, Func<User, Task>>();

        public async Task Sync(UserChangedData userChangedData)
        {
            if (userChangedData == null)
                return;

            if (_actions.ContainsKey(userChangedData.Header.ChangeType))
                await _actions[userChangedData.Header.ChangeType](userChangedData.Payload);
        }

        private async Task DeleteUser(User payload)
        {
            _logger.LogInformation("Delete user = {user}", payload);

            await _userRepository.Delete(payload.Id);
            await _resolutionService.DeleteAllForUser(payload.Id);

            _logger.LogInformation("Delete user completed for {user}", payload);
        }

        private async Task<Guid?> Save(User payload)
        {
            _logger.LogInformation("Save user = {user}", payload);

            await _userRepository.Save(payload);

            _logger.LogInformation("Save user completed for {user}", payload);
            return await Task.FromResult(payload?.Id);
        }
    }
}

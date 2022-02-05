using Microsoft.Extensions.DependencyInjection;
using System;

namespace Publisher.Implementations
{
    internal class PublisherFactory : IPublisherFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PublisherFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IUserChangedReceiver GetUserChangedReceiver()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            try
            {
                return scope.ServiceProvider.GetRequiredService<IUserChangedReceiver>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

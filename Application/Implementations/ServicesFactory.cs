using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application.Implementations
{
    public class ServicesFactory : IApplicationServicesFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServicesFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IResolutionService GetResolutionService()
        {
            return GetService<IResolutionService>();
        }

        public IUserService GetUserService()
        {
            return GetService<IUserService>();
        }

        private T GetService<T>()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            try
            {
                return scope.ServiceProvider.GetRequiredService<T>();
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}

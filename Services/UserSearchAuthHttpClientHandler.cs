using Services.Implementations;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    internal class UserSearchAuthHttpClientHandler : DelegatingHandler
    {
        private readonly UserSearchConfig _configuration;

        public UserSearchAuthHttpClientHandler(UserSearchConfig configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // TODO: Add authentication token.
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
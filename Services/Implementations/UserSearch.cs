using Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class UserSearch : ServiceBase, IUserSearch
    {
        private readonly HttpClient _httpClient;
        private readonly UserSearchConfig _config;
        private readonly IRestClient _restClient;

        public UserSearch(HttpClient httpClient, IRestClient restClient, UserSearchConfig config)
        {
            _httpClient = httpClient;
            _restClient = restClient;
            _config = config;
        }

        public async Task<User> Find(Guid id)
        {
            var endpoint = $"{CreateEndPoint(_config.BaseUrl, _config.FindUri)}{id}";

            var user = await _restClient.Get<User>(_httpClient, endpoint);

            return user.Data;
        }
    }
}

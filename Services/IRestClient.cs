using Services.Responses;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    internal interface IRestClient
    {
        Task<RestCallResponse<T>> Get<T>(HttpClient httpClient, string endpoint);
    }
}

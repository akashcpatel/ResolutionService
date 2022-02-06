using Newtonsoft.Json;
using Polly.CircuitBreaker;
using Services.Responses;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class RestClient : IRestClient
    {
        [DebuggerStepThrough]
        public async Task<RestCallResponse<T>> Get<T>(HttpClient httpClient, string endpoint) =>
            await Execute<T>(endpoint, httpClient.GetAsync);

        private async Task<RestCallResponse<T>> Execute<T>(string endpoint, Func<string, Task<HttpResponseMessage>> asyncRestMethod)
        {
            HttpResponseMessage httpResponse = null;
            try
            {
                httpResponse = await asyncRestMethod(endpoint);
                return await CreateRestCallResponse<T>(httpResponse);
            }
            catch (HttpRequestException requestEx)
            {
                return CreateExceptionRestCallResponse<T>(httpResponse);
            }
            catch (Exception ex) when (ex is BrokenCircuitException)
            {
                return CreateExceptionRestCallResponse<T>(httpResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private RestCallResponse<T> CreateExceptionRestCallResponse<T>(HttpResponseMessage httpResponse)
        {
            return new RestCallResponse<T>
            {
                ResponseCode = httpResponse?.StatusCode == System.Net.HttpStatusCode.NotFound ? ResponseCode.NotFound : ResponseCode.Error
            };
        }

        private async Task<RestCallResponse<T>> CreateRestCallResponse<T>(HttpResponseMessage httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            var callResponse = new RestCallResponse<T>
            {
                Data = httpResponse.StatusCode == System.Net.HttpStatusCode.NoContent ?
                                await Task.FromResult(default(T)) :
                                await ReadFromJsonAsync<T>(httpResponse),
                ResponseCode = ResponseCode.Success
            };

            return callResponse;
        }

        private async Task<T> ReadFromJsonAsync<T>(HttpResponseMessage httpResponse)
        {
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}

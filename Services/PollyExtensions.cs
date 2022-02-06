using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using System;
using System.Linq;
using System.Net;

namespace Services
{
    public static class PollyExtensions
    {
        static readonly HttpStatusCode[] HttpStatusCodesWorthRetrying =
        {
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout
        };

        public static IHttpClientBuilder GetRetryPolicy(this IHttpClientBuilder builder)
        {
            return builder.AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .OrResult(msg => HttpStatusCodesWorthRetrying.Contains(msg.StatusCode))
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(10)));
        }
    }
}

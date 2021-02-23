using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Infrastructure.HttpClient
{
    public class ResilientHttpClient : IResilientHttpClient
    {
        private static AsyncPolicy _circuitBreakerPolicy;
        private static AsyncPolicy _retryPolicy;

        private System.Net.Http.HttpClient _client;
        private IHttpContextAccessor _httpContextAccessor;

        public ResilientHttpClient(IHttpContextAccessor httpContextAccessor, System.Net.Http.HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _client = httpClient;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _circuitBreakerPolicy = Policy.Handle<AggregateException>(x =>
            {
                var result = x.InnerException is HttpRequestException;
                System.Console.WriteLine("Circuit opened...");
                return result;
            })
            .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromSeconds(10));

            _retryPolicy = Policy.Handle<AggregateException>(x =>
            {
                var result = x.InnerException is HttpRequestException;
                return result;
            }).RetryForeverAsync(ex => System.Console.WriteLine("Retrying..."));
        }

        public Task<HttpResponseMessage> GetAsync(string uri, string authToken = null)
        {
            return ExecuteWithRetryandCircuitBreakerAsync(uri, authToken, async () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

                SetAuthHeader(requestMessage);

                if (authToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authToken);
                }

                var response = await _client.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authToken = null)
        {
            return ExecuteWithRetryandCircuitBreakerAsync(uri, authToken, async () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

                SetAuthHeader(requestMessage);

                if (authToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authToken);
                }

                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authToken = null)
        {
            return ExecuteWithRetryandCircuitBreakerAsync(uri, authToken, async () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);

                SetAuthHeader(requestMessage);

                if (authToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authToken);
                }

                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");

                var response = await _client.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri, string authToken = null)
        {
            return ExecuteWithRetryandCircuitBreakerAsync(uri, authToken, async () =>
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

                SetAuthHeader(requestMessage);

                if (authToken != null)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authToken);
                }

                var response = await _client.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }

                return response;
            });
        }

        private async Task<HttpResponseMessage> ExecuteWithRetryandCircuitBreakerAsync(string uri, string authToken, Func<Task<HttpResponseMessage>> func)
        {
            var res = await _retryPolicy.WrapAsync(_circuitBreakerPolicy).ExecuteAsync(() => func());
            return res;
        }

        private void SetAuthHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }
    }
}
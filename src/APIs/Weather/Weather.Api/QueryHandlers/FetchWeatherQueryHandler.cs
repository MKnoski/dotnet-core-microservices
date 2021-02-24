using System;
using MediatR;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Http.HttpClient;
using Weather.Api.Queries;
using Weather.Domain.Models;

namespace Weather.Api.QueryHandlers
{
    public class FetchWeatherQueryHandler : IRequestHandler<FetchWeatherQuery, WeatherApiResponse>
    {
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");
        
        private readonly IResilientHttpClient _httpClient;

        public FetchWeatherQueryHandler(IResilientHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherApiResponse> Handle(FetchWeatherQuery request, CancellationToken cancellationToken)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?appid={ApiKey}&units=meters&q={request.WeatherDelivery.Location}";
            
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponse>(json);

                return weatherApiResponse;
            }

            return null;
        }
    }
}
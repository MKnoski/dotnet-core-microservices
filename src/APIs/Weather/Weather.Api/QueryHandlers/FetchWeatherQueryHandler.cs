using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Http.HttpClient;
using Microsoft.Extensions.Options;
using Weather.Api.Configuration;
using Weather.Api.Queries;
using Weather.Domain.Models;

namespace Weather.Api.QueryHandlers
{
    public class FetchWeatherQueryHandler : IRequestHandler<FetchWeatherQuery, WeatherApiResponse>
    {
        private readonly IOptions<Secrets> _config;
        
        private readonly IResilientHttpClient _httpClient;

        public FetchWeatherQueryHandler(IResilientHttpClient httpClient, IOptions<Secrets> config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<WeatherApiResponse> Handle(FetchWeatherQuery request, CancellationToken cancellationToken)
        {
            var apiKey = _config.Value.OpenWeatherMapApiKey;
            
            var url = $"http://api.openweathermap.org/data/2.5/weather?appid={apiKey}&units=meters&q={request.WeatherDelivery.Location}";
            
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
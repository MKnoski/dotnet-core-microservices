using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Weather.Domain.Models;
using Infrastructure.HttpClient;
using Weather.App.Queries;
using Weather.App.Configuration;
using Weather.App.Notifications;

namespace Weather.App.QueryHandlers
{
    public class FetchWeatherQueryHandler : IRequestHandler<FetchWeatherQuery, WeatherApiResponse>
    {
        private readonly IOptions<Secrets> _config;
        private readonly IResilientHttpClient _httpClient;
        private readonly IMediator _mediator;

        public FetchWeatherQueryHandler(IMediator mediator, IResilientHttpClient httpClient, IOptions<Secrets> config)
        {
            _mediator = mediator;
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<WeatherApiResponse> Handle(FetchWeatherQuery request, CancellationToken cancellationToken)
        {
            var apiKey = _config.Value.OpenWeatherMapApiKey;
            
            var url = $"http://api.openweathermap.org/data/2.5/weather?appid={apiKey}&units=meters&q={request.Location}";
            
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponse>(json);

                await _mediator.Publish(new FetchWeatherNotification(request, weatherApiResponse));

                return weatherApiResponse;
            }

            return null;
        }
    }
}
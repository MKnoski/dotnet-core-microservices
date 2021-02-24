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
        private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?appid=25b4d3146e296514799c8743105a1324&units=meters&q=";

        private readonly IResilientHttpClient _httpClient;

        public FetchWeatherQueryHandler(IResilientHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherApiResponse> Handle(FetchWeatherQuery request, CancellationToken cancellationToken)
        {
            var url = ApiUrl + request.WeatherDelivery.Location;

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponse>(json);
            }

            return null;
        }
    }
}
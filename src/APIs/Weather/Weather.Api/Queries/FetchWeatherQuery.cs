using MediatR;
using Weather.Domain.Models;

namespace Weather.Api.Queries
{
    public class FetchWeatherQuery : IRequest<WeatherApiResponse>
    {
        public FetchWeatherQuery(WeatherDelivery delivery)
        {
            WeatherDelivery = delivery;
        }

        public WeatherDelivery WeatherDelivery { get; }
    }
}
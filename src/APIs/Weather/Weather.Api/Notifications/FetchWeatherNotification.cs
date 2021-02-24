using MediatR;
using Weather.Domain.Models;

namespace Weather.Api.Notifications
{
    public class FetchWeatherNotification : INotification
    {
        public FetchWeatherNotification(WeatherDelivery weatherDelivery, WeatherApiResponse weatherApiResponse)
        {
            WeatherDelivery = weatherDelivery;
            WeatherApiResponse = weatherApiResponse;
        }
        
        public WeatherApiResponse WeatherApiResponse { get; }
        
        public WeatherDelivery WeatherDelivery { get; }
    }
}
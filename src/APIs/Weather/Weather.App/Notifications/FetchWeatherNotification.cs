using MediatR;
using Weather.App.Queries;
using Weather.Domain.Models;

namespace Weather.App.Notifications
{
    public class FetchWeatherNotification : INotification
    {
        public FetchWeatherNotification(FetchWeatherQuery fetchWeatherQuery, WeatherApiResponse weatherApiResponse)
        {
            FetchWeatherQuery = fetchWeatherQuery;
            WeatherApiResponse = weatherApiResponse;
        }
        
        public WeatherApiResponse WeatherApiResponse { get; }
        
        public FetchWeatherQuery FetchWeatherQuery { get; }
    }
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Sdk;
using Infrastructure.Http.HttpClient;
using Infrastructure.Http.HttpModels;
using MediatR;
using Weather.Api.Notifications;

namespace Weather.Api.NotificationHandlers
{
    public class SendEmailHandler : INotificationHandler<FetchWeatherNotification>
    {
        private const string EmailApiUrl = "localhost";
        
        private readonly IResilientHttpClient _resilientHttpClient;

        public SendEmailHandler(IResilientHttpClient resilientHttpClient)
        {
            _resilientHttpClient = resilientHttpClient;
        }
        
        public async Task Handle(FetchWeatherNotification notification, CancellationToken cancellationToken)
        {
            var weather = notification.WeatherApiResponse;
            var location = notification.WeatherDelivery.Location;
            var email = notification.WeatherDelivery.EmailAddress;
            
            var sendEmailModel = new SendEmailHttpModel
            {
                FromEmail = "maks.knoski@gmail.com",
                FromName = "Maks",
                Subject = $"Your weather for {location}",
                To = email,
                PlainTextContent = "",
                HtmlContent = $"<h1>{location} - {weather.weather.FirstOrDefault()?.main}</h1>" +
                              $"<strong>Temp: {weather.main.temp.ToInt()}°C</strong>" + "<br><br>" +
                              $"<strong>Temp feels like: {weather.main.feels_like.ToInt()}°C</strong>" + "<br><br>" +
                              $"<strong>Temp min: {weather.main.temp_max.ToInt()}°C</strong>" + "<br><br>" +
                              $"<strong>Temp min: {weather.main.temp_max.ToInt()}°C</strong>",
            };
            
            await _resilientHttpClient.PostAsync(EmailApiUrl, sendEmailModel);
        }
    }
}
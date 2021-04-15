using MediatR;
using System;
using Weather.Domain.Models;

namespace Weather.App.Queries
{
    public class FetchWeatherQuery : IRequest<WeatherApiResponse>
    {
        public Guid Id { get; set; }

        public string Location { get; set; }

        public string EmailAddress { get; set; }
    }
}
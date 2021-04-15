using System;
using System.Threading.Tasks;
using Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Api.Notifications;
using Weather.Api.Queries;
using Weather.Domain.Models;

namespace Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherDeliveryController : BaseController
    {
        private readonly IMediator _mediator;

        public WeatherDeliveryController(IMediator mediator, ILogger<WeatherDeliveryController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> InitWeatherDelivery(WeatherDelivery weatherDelivery)
        {
            weatherDelivery.Id = Guid.NewGuid();

            Logger.LogInformation($"Weather delivery init - id: {weatherDelivery.Id}");

            var query = new FetchWeatherQuery(weatherDelivery);
            var weatherResponse = await _mediator.Send(query);

            if (weatherResponse == null)
            {
                return NotFound();
            }
            
            await _mediator.Publish(new FetchWeatherNotification(weatherDelivery, weatherResponse));

            return Ok();
        }
    }
}
using System;
using System.Threading.Tasks;
using Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.App.Notifications;
using Weather.App.Queries;

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
        public async Task<IActionResult> InitWeatherDelivery(FetchWeatherQuery request)
        {
            request.Id = Guid.NewGuid();

            Logger.LogInformation($"Weather delivery init - id: {request.Id}");

            var weatherResponse = await _mediator.Send(request);

            if (weatherResponse == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
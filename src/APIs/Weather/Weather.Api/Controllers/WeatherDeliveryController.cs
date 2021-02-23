using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [Authorize]
        public async Task<IActionResult> InitWeatherDelivery(WeatherDelivery delivery)
        {
            delivery.Id = Guid.NewGuid();

            Logger.LogInformation($"Weather delivery init - id: {delivery.Id}");

            var query = new FetchWeatherQuery(delivery);
            var weatherReponse = await _mediator.Send(query);

            if (weatherReponse == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
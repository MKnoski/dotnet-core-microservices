using System.Threading.Tasks;
using Email.Api.Commands;
using Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Email.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : BaseController
    {
        private readonly IMediator _mediator;
        
        public EmailController(ILogger<EmailController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailCommand command)
        {
            await _mediator.Send(command);
            
            return Ok();
        }
    }
}
